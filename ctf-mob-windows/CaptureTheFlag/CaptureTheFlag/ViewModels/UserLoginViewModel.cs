using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using RestSharp;
using System.Reflection;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserLoginViewModel : Screen, IHandle<LoginResponse>, IHandle<ServerErrorMessage>
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IEventAggregator eventAggregator;
        private RestRequestAsyncHandle requestHandle; //TODO: use requestHandle to abort when neccessary

        public UserLoginViewModel(INavigationService navigationService, ICommunicationService communicationService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.eventAggregator = eventAggregator;
            DisplayName = "Login";
            Login = "Login";
            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void LoginAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            requestHandle = communicationService.Login<LoginResponse>(Username, Password, response =>
            {

            });
            
        }
        #endregion

        #region Message Handling
        public void Handle(LoginResponse message)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = true;
            navigationService
                .UriFor<MainAppPivotViewModel>()
                .WithParam(param => param.Token, message.Token)
                .Navigate();
            
        }

        public void Handle(ServerErrorMessage message)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            MessageBox.Show(message.Code.ToString(), message.Message, MessageBoxButton.OK);
            IsFormAccessible = true;
        }
        #endregion

        #region Properties
        private string username;
        public string Username
        { 
            get {  return username; }
            set
            {
                if(username != value)
                {
                    username = value;
                    NotifyOfPropertyChange(() => Username);
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyOfPropertyChange(() => Password);
                }
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    NotifyOfPropertyChange(() => Login);
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
                    eventAggregator.Publish(isFormAccessible);
                }
            }
        }
#endregion
    }
}
