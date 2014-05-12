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

        public ListGamesViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService, ILocationService locationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;

            Games = new BindableCollection<Game>();
            Authenticator = new Authenticator();

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

            IsFormAccessible = true;
        }

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
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Games.Clear();
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
                            if (!globalStorageService.Current.Games.ContainsKey(game.Url))
                            {
                                globalStorageService.Current.Games[game.Url] = game;
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
                if ((SelectedGame.Owner != null) && (SelectedGame.Owner == Authenticator.user))
                {
                    navigationService.UriFor<EditGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.Url)
                         .Navigate();
                }
                else
                {
                    navigationService.UriFor<ShowGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.Url)
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

        //TODO: Consider caliburn micro "CanExecute" functionality
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
