namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using Microsoft.Phone.Notification;
    using RestSharp;
    using System;
    using System.Device.Location;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;

    public class ListGamesViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly ILocationService locationService; //TODO: move
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        HttpNotificationChannel pushChannel = null;

        public ListGamesViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService, ILocationService locationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;

            Games = new BindableCollection<Game>();
            Authenticator = new Authenticator();



            IsFormAccessible = true;

            DisplayName = "Games";

            FindAppBarItemText = "find";
            FindIcon = new Uri("/Images/feature.search.png", UriKind.Relative);
            CreateAppBarItemText = "create";
            CreateIcon = new Uri("/Images/add.png", UriKind.Relative);
            RefreshAppBarItemText = "refresh";
            RefreshIcon = new Uri("/Images/refresh.png", UriKind.Relative);

            SettingsAppBarMenuItemText = "settings";
            CharactersAppBarMenuItemText = "characters";
            ProfileAppBarMenuItemText = "profile";
        }

        //NOTE: Notification is recieved as HttpNotification regardless it it is toast or not (Toast is recieved in Windows 8.1 Universal App on phone)
        private void RegisterNotificationChannel()
        {
            string channelName = "ctf/aplha/notification/wp";
            pushChannel = HttpNotificationChannel.Find(channelName);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(pushChannel_HttpNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(pushChannel_HttpNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}",
                    pushChannel.ChannelUri.ToString()));

            }

            
        }

        void pushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var sr = new StreamReader(e.Notification.Body);
                var myStr = sr.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(myStr);
            });
        }

        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            // Display a dialog of all the fields in the toast.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message.ToString());
            });
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}", e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
            });
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", e.ChannelUri.ToString()));
            });
        }

        //TODO: move to location service
        #region Watcher
        private GeoCoordinateWatcher watcher;
        public GeoCoordinateWatcher Watcher
        {
            get { return watcher; }
            set
            {
                if (watcher != value)
                {
                    watcher = value;
                    NotifyOfPropertyChange(() => Watcher);
                }
            }
        }
        public void RegisterPositionAction()
        {
            //TODO: Response object model
            Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            Watcher.Start();
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.RegisterPosition(Games.FirstOrDefault(), Watcher.Position.Location, Authenticator.token,
                    rData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                    }
                );
            }
            Watcher.Stop();
        }
        #endregion

        #region Screen states
        protected override void OnActivate()
        {
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            if (globalStorageService.Current.Games != null && globalStorageService.Current.Games.Count > 0)
            {
                foreach (Game game in globalStorageService.Current.Games.Values)
                {
                    Games.Add(game);
                }
            }
            else
            {
                ListGamesAction();
            }
            RegisterNotificationChannel();
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Games.Clear();
            if(pushChannel != null)
            {
                pushChannel.Close();
            }
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void ListGamesAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.GetAllGames(Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Games = responseData;
                        foreach (Game game in Games)
                        {
                            if (!globalStorageService.Current.Games.ContainsKey(game.url))
                            {
                                globalStorageService.Current.Games[game.url] = game;
                            }
                        }
                        IsFormAccessible = true;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                        IsFormAccessible = true;
                    }
                );
            }
        }

        public void ReadGameAction()
        {
            if (SelectedGame != null && Authenticator.IsValid(Authenticator))
            {
#warning operator != is for tests, should be ==
                if (SelectedGame.url != Authenticator.user)
                {
                    navigationService.UriFor<EditGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.url)
                         .Navigate();
                }
                else
                {
                    navigationService.UriFor<ShowGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.url)
                         .Navigate();
                }
                SelectedGame = null;
            }
        }

        public void FindGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<SearchGameViewModel>().Navigate();
        }

        public void CreateGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<CreateGameViewModel>().Navigate();
        }

        public void RefreshGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            ListGamesAction();
        }

        //public void CharactersAction()
        //{
        //    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        //    navigationService.UriFor<CharacterViewModel>().Navigate();
        //}

        public void ProfileAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<UserViewModel>().Navigate();
        }

        public void SettingsAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            //TODO: Settings
        }
        #endregion

        #region Properties
        private Authenticator authenticator;
        public Authenticator Authenticator
        {
            get { return authenticator; }
            set
            {
                if (authenticator != value)
                {
                    authenticator = value;
                    NotifyOfPropertyChange(() => Authenticator);
                }
            }
        }

        #region Model Properties
        private BindableCollection<Game> games;
        public BindableCollection<Game> Games
        {
            get { return games; }
            set
            {
                if (games != value)
                {
                    games = value;
                    NotifyOfPropertyChange(() => Games);
                }
            }
        }

        private Game selectedGame;
        public Game SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (selectedGame != value)
                {
                    selectedGame = value;
                    NotifyOfPropertyChange(() => SelectedGame);
                }
            }
        }
        #endregion

        #region UI properties
        private Uri findIcon;
        public Uri FindIcon
        {
            get { return findIcon; }
            set
            {
                if (findIcon != value)
                {
                    findIcon = value;
                    NotifyOfPropertyChange(() => FindIcon);
                }
            }
        }

        private Uri createIcon;
        public Uri CreateIcon
        {
            get { return createIcon; }
            set
            {
                if (createIcon != value)
                {
                    createIcon = value;
                    NotifyOfPropertyChange(() => CreateIcon);
                }
            }
        }

        private Uri refreshIcon;
        public Uri RefreshIcon
        {
            get { return refreshIcon; }
            set
            {
                if (refreshIcon != value)
                {
                    refreshIcon = value;
                    NotifyOfPropertyChange(() => RefreshIcon);
                }
            }
        }

        private string findAppBarItemText;
        public string FindAppBarItemText
        {
            get { return findAppBarItemText; }
            set
            {
                if (findAppBarItemText != value)
                {
                    findAppBarItemText = value;
                    NotifyOfPropertyChange(() => FindAppBarItemText);
                }
            }
        }

        private string createAppBarItemText;
        public string CreateAppBarItemText
        {
            get { return createAppBarItemText; }
            set
            {
                if (createAppBarItemText != value)
                {
                    createAppBarItemText = value;
                    NotifyOfPropertyChange(() => CreateAppBarItemText);
                }
            }
        }

        private string refreshAppBarItemText;
        public string RefreshAppBarItemText
        {
            get { return refreshAppBarItemText; }
            set
            {
                if (refreshAppBarItemText != value)
                {
                    refreshAppBarItemText = value;
                    NotifyOfPropertyChange(() => RefreshAppBarItemText);
                }
            }
        }

        private string charactersAppBarMenuItemText;
        public string CharactersAppBarMenuItemText
        {
            get { return charactersAppBarMenuItemText; }
            set
            {
                if (charactersAppBarMenuItemText != value)
                {
                    charactersAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => CharactersAppBarMenuItemText);
                }
            }
        }

        private string profileAppBarMenuItemText;
        public string ProfileAppBarMenuItemText
        {
            get { return profileAppBarMenuItemText; }
            set
            {
                if (profileAppBarMenuItemText != value)
                {
                    profileAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => ProfileAppBarMenuItemText);
                }
            }
        }

        private string settingsAppBarMenuItemText;
        public string SettingsAppBarMenuItemText
        {
            get { return settingsAppBarMenuItemText; }
            set
            {
                if (settingsAppBarMenuItemText != value)
                {
                    settingsAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => SettingsAppBarMenuItemText);
                }
            }
        }

        private bool isFormAccessible;
        public bool IsFormAccessible
        {
            get { return isFormAccessible; }
            set
            {
                if (isFormAccessible != value)
                {
                    isFormAccessible = value;
                    NotifyOfPropertyChange(() => IsFormAccessible);
                }
            }
        }
        #endregion
        #endregion
    }
}
