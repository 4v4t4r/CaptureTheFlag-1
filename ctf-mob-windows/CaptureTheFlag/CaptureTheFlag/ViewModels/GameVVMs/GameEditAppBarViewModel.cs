using Caliburn.Micro;
using CaptureTheFlag.Messages;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using RestSharp;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameEditAppBarViewModel : Screen, IHandle<PublishModelResponse<PreGame>>
    {
        private readonly INavigationService navigationService;
        private readonly CommunicationService communicationService;
        private readonly GlobalStorageService globalStorageService;
        private readonly IEventAggregator eventAggregator;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public GameEditAppBarViewModel(INavigationService navigationService, CommunicationService communicationService, GlobalStorageService globalStorageService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;
            this.eventAggregator = eventAggregator;

            //TODO: Implement can execute for actions
            UpdateAppBarItemText = "update";
            UpdateIcon = new Uri("/Images/upload.png", UriKind.Relative);

            StartGameAppBarItemText = "start";
            StartGameIcon = new Uri("/Images/share.png", UriKind.Relative);

            DeleteAppBarMenuItemText = "delete";

            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
            Authenticator = globalStorageService.Current.Authenticator;
            ReadAction();
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Message handling
        public void Handle(PublishModelResponse<PreGame> message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (message.SenderId == this.GetHashCode())
            {
                message.Action(message.Model);
            }
        }
        #endregion

        #region Actions
        public void ReadAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.GetPreGame);
            eventAggregator.Publish(message);
        }

        public void DeleteAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.DeletePreGame);
            eventAggregator.Publish(message);
        }

        public void UpdateAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.PatchPreGame);
            eventAggregator.Publish(message);
        }

        public void StartGameAction()
        {
            PublishModelRequest<PreGame> message = new PublishModelRequest<PreGame>(this, this.StartGame);
            eventAggregator.Publish(message);
        }


        public async void GetPreGame(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                IRestResponse response = await communicationService.GetGameAsync(Authenticator.token, game);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "READ: {0}", response.Content);
                    PreGame responseGame = new CommunicationService.JsondotNETDeserializer().Deserialize<PreGame>(response);
                    eventAggregator.Publish(responseGame);
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }

        public async void DeletePreGame(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                IRestResponse response = await communicationService.DeleteGameAsync(Authenticator.token, game);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "DELETED: {0}", response.Content);
                    PreGame responseGame = new CommunicationService.JsondotNETDeserializer().Deserialize<PreGame>(response);
                    if (navigationService.CanGoBack)
                    {
                        navigationService.GoBack();
                        navigationService.RemoveBackEntry();
                    }
                    MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }

        public async void PatchPreGame(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                IRestResponse response = await communicationService.PatchGameAsync(Authenticator.token, game);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    PreGame responseGame = new CommunicationService.JsondotNETDeserializer().Deserialize<PreGame>(response);
                    eventAggregator.Publish(responseGame); //Not necessary?
                    MessageBox.Show("OK", "updated", MessageBoxButton.OK);
                }
                else
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                    //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<GameErrorType>(response);
                }
                IsFormAccessible = true;
            }
        }

        public void StartGame(PreGame game)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService
                .UriFor<InGameMapViewModel>()
                .WithParam(param => param.GameModelKey, game.Url)
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
        #endregion

        #region UI Properties
        private string deleteAppBarMenuItemText;
        public string DeleteAppBarMenuItemText
        {
            get { return deleteAppBarMenuItemText; }
            set
            {
                if (deleteAppBarMenuItemText != value)
                {
                    deleteAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => DeleteAppBarMenuItemText);
                }
            }
        }

        private Uri updateIcon;
        public Uri UpdateIcon
        {
            get { return updateIcon; }
            set
            {
                if (updateIcon != value)
                {
                    updateIcon = value;
                    NotifyOfPropertyChange(() => UpdateIcon);
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

        private string updateAppBarItemText;
        public string UpdateAppBarItemText
        {
            get { return updateAppBarItemText; }
            set
            {
                if (updateAppBarItemText != value)
                {
                    updateAppBarItemText = value;
                    NotifyOfPropertyChange(() => UpdateAppBarItemText);
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
