using Caliburn.Micro;
using CaptureTheFlag.Messages;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameMapVVMs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameCreateAppBarViewModel : Screen, IHandle<PublishModelResponse<PreGame>>
    {
        private readonly INavigationService navigationService;
        private readonly CommunicationService communicationService;
        private readonly GlobalStorageService globalStorageService;
        private readonly IEventAggregator eventAggregator;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public GameCreateAppBarViewModel(INavigationService navigationService, CommunicationService communicationService, GlobalStorageService globalStorageService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;
            this.eventAggregator = eventAggregator;

            IsFormAccessible = true;

            Authenticator = new Authenticator();
            GameModelKey = "TemporaryGameModelKey";

            DisplayName = "Create game";

            //Create game state:
            GameMapAppBarItemText = "Game map";
            GameMapIcon = new Uri("/Images/like.png", UriKind.Relative);

            CreateAppBarItemText = "Done";
            CreateIcon = new Uri("/Images/check.png", UriKind.Relative);
        }

        #region Actions
        public void GameMapAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.CreatePreGame);
            eventAggregator.Publish(message);
        }

        public void CreateAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.OpenGameMap);
            eventAggregator.Publish(message);
        }
        //public async void CommunicationConceptTODO()
        //{
        //    //requestHandle = communicationService.CreateGame();
        //    //Game = await responseGame;
        //    //foreach (Item item in Items) { item.Url = Game.Url; communicationService.CreateItem(); }
        //    //foreach (responseItem) Game.Items.Add(await item)
        //    //await UpdateGame()
        //}

        //TODO: If properly simplifiedand added cancelling implement this approach in every communication action
        public async void CreatePreGame(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                //globalStorageService.Current.Games[GameModelKey].Radius = 100.0;
                game.Radius = 100.0;
                IRestResponse response = await communicationService.CreateGameAsync(Authenticator.token, game);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "CREATED: {0}", response.Content);
                    game = new CommunicationService.JsondotNETDeserializer().Deserialize<PreGame>(response);
                    if (Items != null)
                    {
                        foreach (Item item in Items)
                        {
                            item.Game = game.Url;
                            response = await communicationService.CreateItemAsync(Authenticator.token, item);
                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "CREATED: {0}", response.Content);
                                Item responseItem = new CommunicationService.JsondotNETDeserializer().Deserialize<Item>(response);
                                game.Items.Add(responseItem.Url);
                            }
                            else
                            {
                                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                                //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                                return;
                            }
                        }
                        PreGame patchGame = new PreGame() { Url = game.Url, Items = game.Items };
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
                        navigationService.UriFor<MainAppPivotViewModel>()
                            .Navigate();
                        MessageBox.Show("OK", "created", MessageBoxButton.OK);
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

        public void OpenGameMap(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<CreateGameMapViewModel>()
                .WithParam(param => param.GameModelKey, game.Url)
                .Navigate();
        }
        #endregion

        #region Message handling
        public void Handle(GameModelMessage message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            GameModelKey = message.GameModelKey;
            switch (message.Status)
            {
                case GameModelMessage.STATUS.UPDATED:
                    navigationService.UriFor<CreateGameMapViewModel>()
                        .WithParam(param => param.GameModelKey, GameModelKey)
                        .Navigate();
                    break;
            }
        }

        public void Handle(PublishModelResponse<PreGame> message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (message.SenderId == this.GetHashCode())
            {
                message.Action(message.Model);
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

        public BindableCollection<Item> Items
        {
            get { return globalStorageService.Current.Items; }
        }
        #endregion

        #region UI Properties
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

        //TODO: remove when can execute is available
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
