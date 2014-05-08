namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using CaptureTheFlag.ViewModels.GameVVMs;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Windows;

    public class ShowGameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public ShowGameMapViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            GameMap = new GameMap();
            User = new UserD();
            Authenticator = new Authenticator();

            DisplayName = "Map Details";

            AuthorTextBlock = "Author:";
            NameTextBlock = "Name:";
            DescriptionTextBlock = "Description:";
            RadiusTextBlock = "Radius:";
            LatTextBlock = "Latitude:";
            LonTextBlock = "Longitude:";
            GamesTextBlock = "Games:";

            ChooseMapAppBarItemText = "choose";
            ChooseMapIcon = new Uri("/Images/check.png", UriKind.Relative);
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Authenticator = globalStorageService.Current.Authenticator;
            GameMap.url = GameMapModelKey;

            if (!String.IsNullOrEmpty(GameMap.url))
            {
                if (globalStorageService.Current.GameMaps.ContainsKey(GameMap.url))
                {
                    GameMap = globalStorageService.Current.GameMaps[GameMap.url];
                    User.url = GameMap.author;
                    if (globalStorageService.Current.Users.ContainsKey(User.url))
                    {
                        User = globalStorageService.Current.DUsers[User.url];
                    }
                    else
                    {
                        ReadUserAction();
                    }
                }
                else
                {
                    ReadGameMapAction();
                }
            }
        }
        #endregion

        #region Actions
        public void ReadGameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                communicationService.ReadGameMap(GameMap, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        GameMap = responseData;
                        globalStorageService.Current.GameMaps[GameMap.url] = GameMap;
                        User.url = GameMap.author;
                        ReadUserAction();
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void ReadUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                communicationService.ReadUser(User, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        User = responseData;
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void ChooseMapAction()
        {
            globalStorageService.Current.GameMaps[GameMap.url] = GameMap;
            navigationService.UriFor<CreateGameViewModel>()
                .WithParam(param => param.GameModelKey, GameMap.url)
                .Navigate();
        }
        #endregion

        #region Properties

        #region Model Properties
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

        private GameMap gameMap;
        public GameMap GameMap
        {
            get { return gameMap; }
            set
            {
                if (gameMap != value)
                {
                    gameMap = value;
                    NotifyOfPropertyChange(() => GameMap);
                }
            }
        }

        private UserD user;
        public UserD User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    NotifyOfPropertyChange(() => User);
                }
            }
        }

        private string gameMapModelKey;
        public string GameMapModelKey
        {
            get { return gameMapModelKey; }
            set
            {
                if (gameMapModelKey != value)
                {
                    gameMapModelKey = value;
                    NotifyOfPropertyChange(() => GameMapModelKey);
                }
            }
        }
        #endregion

        #region UI Properties
        private string authorTextBlock;
        public string AuthorTextBlock
        {
            get { return authorTextBlock; }
            set
            {
                if (authorTextBlock != value)
                {
                    authorTextBlock = value;
                    NotifyOfPropertyChange(() => AuthorTextBlock);
                }
            }
        }

        private string nameTextBlock;
        public string NameTextBlock
        {
            get { return nameTextBlock; }
            set
            {
                if (nameTextBlock != value)
                {
                    nameTextBlock = value;
                    NotifyOfPropertyChange(() => NameTextBlock);
                }
            }
        }

        private string descriptionTextBlock;
        public string DescriptionTextBlock
        {
            get { return descriptionTextBlock; }
            set
            {
                if (descriptionTextBlock != value)
                {
                    descriptionTextBlock = value;
                    NotifyOfPropertyChange(() => DescriptionTextBlock);
                }
            }
        }

        private string radiusTextBlock;
        public string RadiusTextBlock
        {
            get { return radiusTextBlock; }
            set
            {
                if (radiusTextBlock != value)
                {
                    radiusTextBlock = value;
                    NotifyOfPropertyChange(() => RadiusTextBlock);
                }
            }
        }

        private string latTextBlock;
        public string LatTextBlock
        {
            get { return latTextBlock; }
            set
            {
                if (latTextBlock != value)
                {
                    latTextBlock = value;
                    NotifyOfPropertyChange(() => LatTextBlock);
                }
            }
        }

        private string lonTextBlock;
        public string LonTextBlock
        {
            get { return lonTextBlock; }
            set
            {
                if (lonTextBlock != value)
                {
                    lonTextBlock = value;
                    NotifyOfPropertyChange(() => LonTextBlock);
                }
            }
        }

        private string gamesTextBlock;
        public string GamesTextBlock
        {
            get { return gamesTextBlock; }
            set
            {
                if (gamesTextBlock != value)
                {
                    gamesTextBlock = value;
                    NotifyOfPropertyChange(() => GamesTextBlock);
                }
            }
        }

        private Uri chooseMapIcon;
        public Uri ChooseMapIcon
        {
            get { return chooseMapIcon; }
            set
            {
                if (chooseMapIcon != value)
                {
                    chooseMapIcon = value;
                    NotifyOfPropertyChange(() => ChooseMapIcon);
                }
            }
        }

        private string chooseMapAppBarItemText;
        public string ChooseMapAppBarItemText
        {
            get { return chooseMapAppBarItemText; }
            set
            {
                if (chooseMapAppBarItemText != value)
                {
                    chooseMapAppBarItemText = value;
                    NotifyOfPropertyChange(() => ChooseMapAppBarItemText);
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
