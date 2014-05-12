namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using CaptureTheFlag.ViewModels.GameMapVVMs;
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    public class CreateGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public CreateGameViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            Game = new Game();
            Authenticator = new Authenticator();
            GameModelKey = "TemporaryGameModelKey";

            DisplayName = "Create game";

            //Create game state:
            GameMapAppBarItemText = "Game map";
            GameMapIcon = new Uri("/Images/like.png", UriKind.Relative);

            CreateAppBarItemText = "Done";
            CreateIcon = new Uri("/Images/check.png", UriKind.Relative);

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
            if (!String.IsNullOrEmpty(GameModelKey) && globalStorageService.Current.Games.ContainsKey(GameModelKey))
            {
                Game = globalStorageService.Current.Games[GameModelKey];
            }
        }
        #endregion

        #region Actions

        //public async void CommunicationConceptTODO()
        //{
        //    //requestHandle = communicationService.CreateGame();
        //    //Game = await responseGame;
        //    //foreach (Item item in Items) { item.Url = Game.Url; communicationService.CreateItem(); }
        //    //foreach (responseItem) Game.Items.Add(await item)
        //    //await UpdateGame()
        //}

        //TODO: If properly simplifiedand added cancelling implement this approach in every communication action
        public async void CreateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                Game.Radius = 100.0;
                IRestResponse response = await communicationService.CreateGameAsync(Authenticator.token, Game);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "CREATED: {0}", response.Content);
                    Game = new CommunicationService.JsondotNETDeserializer().Deserialize<Game>(response);
                    foreach(Item item in Items)
                    {
                        item.Game = Game.Url;
                        response = await communicationService.CreateItemAsync(Authenticator.token, item);
                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "CREATED: {0}", response.Content);
                            Item responseItem = new CommunicationService.JsondotNETDeserializer().Deserialize<Item>(response);
                            Game.Items.Add(responseItem.Url);
                        }
                        else
                        {
                            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                            //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                            return;
                        }
                    }
                    Game patchGame = new Game() { Url = Game.Url, Items = Game.Items };
                    response = await communicationService.PatchGameAsync(Authenticator.token, patchGame);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                        navigationService.UriFor<MainAppPivotViewModel>()
                            .Navigate();
                        MessageBox.Show("OK", "created", MessageBoxButton.OK);
                    }
                    else
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                        //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<GameErrorType>(response);
                    }
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<GameErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }

        //public void CreateAction()
        //{
        //    //TODO: On successremove from gameCache gameMapModelKey as it is temporary!!!
        //    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        //    IsFormAccessible = false;
        //    if (Authenticator.IsValid(Authenticator))
        //    {
        //        requestHandle = communicationService.CreateGame(Game, Authenticator.token,
        //            responseGameMap =>
        //            {
        //                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
        //                Game = responseGameMap;

        //                globalStorageService.Current.Games[Game.Url] = Game;
        //                globalStorageService.Current.Games.Remove(GameModelKey);
        //                IsFormAccessible = true;
        //                navigationService.UriFor<MainAppPivotViewModel>()
        //                    .Navigate();
        //                MessageBox.Show("OK", "created", MessageBoxButton.OK);
        //            },
        //            serverErrorMessage =>
        //            {
        //                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
        //                IsFormAccessible = true;
        //                MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
        //            }
        //        );
        //    }
        //}

        //public void AddItemsRequestAction()
        //{
        //    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        //    IsFormAccessible = false;
        //    if (Authenticator.IsValid(Authenticator))
        //    {
        //        foreach (Item item in Items)
        //        {
        //            item.Game = Game.Url;
        //            communicationService.CreateItem(item, Authenticator.token,
        //                responseData =>
        //                {
        //                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
        //                    Items.Add(responseData);

        //                },
        //                serverErrorMessage =>
        //                {
        //                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
        //                    IsFormAccessible = true;
        //                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
        //                }
        //            );
        //        }
        //    }
        //}

        public void GameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            globalStorageService.Current.Games[GameModelKey] = Game;
            navigationService.UriFor<CreateGameMapViewModel>()
                .WithParam(param => param.GameModelKey, GameModelKey)
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

        private Game game;
        public Game Game
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

        public string SelectedType
        {
            get
            {
                return Game.Types.FirstOrDefault(pair => pair.Value == Game.Type).Key;
            }
            set
            {
                Game.Type = Game.Types[value];
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        public BindableCollection<Item> Items
        {
            get { return globalStorageService.Current.Items; }
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

        private Uri gameMapIcon;
        public Uri GameMapIcon
        {
            get { return gameMapIcon; }
            set
            {
                if (gameMapIcon != value)
                {
                    gameMapIcon = value;
                    NotifyOfPropertyChange(() => GameMapIcon);
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

        private string gameMapAppBarItemText;
        public string GameMapAppBarItemText
        {
            get { return gameMapAppBarItemText; }
            set
            {
                if (gameMapAppBarItemText != value)
                {
                    gameMapAppBarItemText = value;
                    NotifyOfPropertyChange(() => GameMapAppBarItemText);
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
