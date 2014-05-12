using Caliburn.Micro;
using CaptureTheFlag.Messages;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using RestSharp;
using System;
using System.Reflection;
using System.Windows;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameDetailsAppBarViewModel : Screen, IHandle<GameModelMessage>
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private readonly IEventAggregator eventAggregator;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public GameDetailsAppBarViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;
            this.eventAggregator = eventAggregator;

            //TODO: Implement can execute for actions
            AddUserAppBarItemText = "subscribe";
            AddUserIcon = new Uri("/Images/add.png", UriKind.Relative);

            RemoveUserAppBarItemText = "unsubscribe";
            RemoveUserIcon = new Uri("/Images/minus.png", UriKind.Relative);

            StartGameAppBarItemText = "start";
            StartGameIcon = new Uri("/Images/share.png", UriKind.Relative);

            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
            Authenticator = globalStorageService.Current.Authenticator;
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Message handling
        public void Handle(GameModelMessage message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            GameModelKey = message.GameModelKey;
            switch (message.Status)
            {
                case GameModelMessage.STATUS.SHOULD_GET:
                    if (!String.IsNullOrEmpty(GameModelKey) && !globalStorageService.Current.Games.ContainsKey(GameModelKey))
                    {
                        ReadAction();
                    }
                    else
                    {
                        eventAggregator.Publish(new GameModelMessage() { GameModelKey = GameModelKey, Status = ModelMessage.STATUS.IN_STORAGE });
                    }
                    break;
                case GameModelMessage.STATUS.UPDATED:
                    break;
            }
        }
        #endregion

        #region Actions
        public async void ReadAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                IRestResponse response = await communicationService.GetGameAsync(Authenticator.token, new PreGame() { Url = GameModelKey });
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "READ: {0}", response.Content);
                    PreGame responseGame = new CommunicationService.JsondotNETDeserializer().Deserialize<PreGame>(response);
                    globalStorageService.Current.Games[responseGame.Url] = responseGame;
                    eventAggregator.Publish(new GameModelMessage() { GameModelKey = responseGame.Url, Status = ModelMessage.STATUS.IN_STORAGE });
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }

        public void AddUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.AddPlayerToGame(globalStorageService.Current.Games[GameModelKey], Authenticator.token,
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
                requestHandle = communicationService.RemovePlayerFromGame(globalStorageService.Current.Games[GameModelKey], Authenticator.token,
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

        public void StartGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService
                .UriFor<GeoMapViewModel>()
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
        #endregion

        #region UI Properties
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

        private Uri startGameIcon;
        public Uri StartGameIcon
        {
            get { return startGameIcon; }
            set
            {
                if (startGameIcon != value)
                {
                    startGameIcon = value;
                    NotifyOfPropertyChange(() => StartGameIcon);
                }
            }
        }

        private string startGameAppBarItemText;
        public string StartGameAppBarItemText
        {
            get { return startGameAppBarItemText; }
            set
            {
                if (startGameAppBarItemText != value)
                {
                    startGameAppBarItemText = value;
                    NotifyOfPropertyChange(() => StartGameAppBarItemText);
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
