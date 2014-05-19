using Caliburn.Micro;
using CaptureTheFlag.Messages;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameVVMs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class GamesListAppBarViewModel : Screen, IHandle<PreGame> //TODO: RequestAction with Tap, Click ect. param
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly CommunicationService communicationService;
        private readonly GlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public GamesListAppBarViewModel(INavigationService navigationService, CommunicationService communicationService, GlobalStorageService globalStorageService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;
            this.eventAggregator = eventAggregator;

            Authenticator = new Authenticator();

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


        public void Handle(PreGame message)
        {
            SelectedGame = message;
            ReadGameAction();
        }

        #region Screen states
        protected override void OnActivate()
        {
            base.OnActivate();
            eventAggregator.Subscribe(this);
            Authenticator = globalStorageService.Current.Authenticator;
            //if (globalStorageService.Current.Games != null && globalStorageService.Current.Games.Count > 0)
            //{
            //    foreach (Game game in globalStorageService.Current.Games.Values)
            //    {
            //        Games.Add(game);
            //    }
            //}
            //else
            //{
            ListGamesAction();
            //}
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            //Games.Clear();
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public async void ListGamesAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {

                IRestResponse response = await communicationService.GetAllGamesAsync(Authenticator.token);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "READ: {0}", response.Content);
                    BindableCollection<PreGame> responseListGames = new CommunicationService.JsondotNETDeserializer().Deserialize<BindableCollection<PreGame>>(response);
                    //globalStorageService.Current.GamesList = responseListGames;
                    eventAggregator.Publish(responseListGames);
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }
        public void ReadGameAction()
        {
            if (SelectedGame != null && Authenticator.IsValid(Authenticator))
            {
                if ((SelectedGame.Owner != null) && (SelectedGame.Owner == Authenticator.user))
                {
                    navigationService.UriFor<GameEditScreenViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.Url)
                         .Navigate();
                }
                else
                {
                    navigationService.UriFor<GameDetailsScreenViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.Url)
                         .Navigate();
                }
                SelectedGame = null;
            }
        }

        public void RefreshGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            ListGamesAction();
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
        private BindableCollection<PreGame> games;
        public BindableCollection<PreGame> Games
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

        private PreGame selectedGame;
        public PreGame SelectedGame
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
