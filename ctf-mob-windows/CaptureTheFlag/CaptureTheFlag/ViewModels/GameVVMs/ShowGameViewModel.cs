namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Windows;

    public class ShowGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public ShowGameViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            Game = new GameD();
            Authenticator = new Authenticator();
            //TODO: update to model probably
            StartDate = DateTime.Now.AddDays(1);

            DisplayName = "Game Details";

            AddUserAppBarItemText = "subscribe me";
            AddUserIcon = new Uri("/Images/add.png", UriKind.Relative);

            RemoveUserAppBarItemText = "unsubscribe me";
            RemoveUserIcon = new Uri("/Images/minus.png", UriKind.Relative);

            NameTextBlock = "Name:";
            DescriptionTextBlock = "Description:";
            StartTimeTextBlock = "Start time:";
            MaxPlayersTextBlock = "Max players:";
            GameTypeTextBlock = "Game type:";
            VisibilityRangeTextBlock = "Visibility range:";
            ActionRangeTextBlock = "Action range:";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            Game.url = GameModelKey;

            if( ! String.IsNullOrEmpty( Game.url ) )
            {
                if (globalStorageService.Current.GamesD.ContainsKey(Game.url))
                {
                    Game = globalStorageService.Current.GamesD[Game.url];
                }
                else
                {
                    ReadAction();
                }
            }
        }
        #endregion

        #region Actions
 
        public void AddUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.AddPlayerToGame(Game, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "added", MessageBoxButton.OK);
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

        public void RemoveUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.RemovePlayerFromGame(Game, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "added", MessageBoxButton.OK);
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

        public void ReadAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.ReadGame(Game, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        Game = responseData;
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

        private GameD game;
        public GameD Game
        {
            get { return game; }
            set
            {
                if (game != value)
                {
                    game = value;
                    NotifyOfPropertyChange(() => Game);
                }
            }
        }

        private string gameModelKey;
        public string GameModelKey
        {
            get { return gameModelKey; }
            set
            {
                if (gameModelKey != value)
                {
                    gameModelKey = value;
                    NotifyOfPropertyChange(() => GameModelKey);
                }
            }
        }

        //TODO: date time in model maybe?
        //TODO: make aconverter
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    if (Game != null)
                    {
                        Game.start_time = startDate.ToString("s");
                    }
                    NotifyOfPropertyChange(() => StartDate);
                }
            }
        }
        #endregion

        #region UI Properties
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

        private string startTimeTextBlock;
        public string StartTimeTextBlock
        {
            get { return startTimeTextBlock; }
            set
            {
                if (startTimeTextBlock != value)
                {
                    startTimeTextBlock = value;
                    NotifyOfPropertyChange(() => StartTimeTextBlock);
                }
            }
        }

        private string maxPlayersTextBlock;
        public string MaxPlayersTextBlock
        {
            get { return maxPlayersTextBlock; }
            set
            {
                if (maxPlayersTextBlock != value)
                {
                    maxPlayersTextBlock = value;
                    NotifyOfPropertyChange(() => MaxPlayersTextBlock);
                }
            }
        }

        private string gameTypeTextBlock;
        public string GameTypeTextBlock
        {
            get { return gameTypeTextBlock; }
            set
            {
                if (gameTypeTextBlock != value)
                {
                    gameTypeTextBlock = value;
                    NotifyOfPropertyChange(() => GameTypeTextBlock);
                }
            }
        }

        private string visibilityRangeTextBlock;
        public string VisibilityRangeTextBlock
        {
            get { return visibilityRangeTextBlock; }
            set
            {
                if (visibilityRangeTextBlock != value)
                {
                    visibilityRangeTextBlock = value;
                    NotifyOfPropertyChange(() => VisibilityRangeTextBlock);
                }
            }
        }

        private string actionRangeTextBlock;
        public string ActionRangeTextBlock
        {
            get { return actionRangeTextBlock; }
            set
            {
                if (actionRangeTextBlock != value)
                {
                    actionRangeTextBlock = value;
                    NotifyOfPropertyChange(() => ActionRangeTextBlock);
                }
            }
        }

        private string addUserAppBarItemText;
        public string AddUserAppBarItemText
        {
            get { return addUserAppBarItemText; }
            set
            {
                if (addUserAppBarItemText != value)
                {
                    addUserAppBarItemText = value;
                    NotifyOfPropertyChange(() => AddUserAppBarItemText);
                }
            }
        }

        private Uri addUserIcon;
        public Uri AddUserIcon
        {
            get { return addUserIcon; }
            set
            {
                if (addUserIcon != value)
                {
                    addUserIcon = value;
                    NotifyOfPropertyChange(() => AddUserIcon);
                }
            }
        }

        private Uri removeUserIcon;
        public Uri RemoveUserIcon
        {
            get { return removeUserIcon; }
            set
            {
                if (removeUserIcon != value)
                {
                    removeUserIcon = value;
                    NotifyOfPropertyChange(() => RemoveUserIcon);
                }
            }
        }

        private string removeUserAppBarItemText;
        public string RemoveUserAppBarItemText
        {
            get { return removeUserAppBarItemText; }
            set
            {
                if (removeUserAppBarItemText != value)
                {
                    removeUserAppBarItemText = value;
                    NotifyOfPropertyChange(() => RemoveUserAppBarItemText);
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
