using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using RestSharp;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserRegistrationViewModel : Screen, IHandle<RegisterResponse>, IHandle<ServerErrorMessage>
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IEventAggregator eventAggregator;
        private RestRequestAsyncHandle requestHandle; //TODO: use requestHandle to abort when neccessary

        public UserRegistrationViewModel(INavigationService navigationService, ICommunicationService communicationService, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.eventAggregator = eventAggregator;
            DisplayName = "Registration";
            Register = "Register";
            IsFormAccessible = true;
        }

        #region Actions
        public void RegisterAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            requestHandle = communicationService.Register<RegisterResponse>(Username, Password, Email, response =>
            {
                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
                Debug.WriteLine("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription);
                Debug.WriteLine(response.Content);
                Debug.WriteLine("Register method response");
                //TODO: Send a message for navigation
            });
            //navigationService
            //    .UriFor<MainAppPivotViewModel>()
            //    .WithParam(param => param.Name, "Register")
            //    .Navigate();
        }
        #endregion

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);

            base.OnDeactivate(close);
        }
        #endregion

        #region Message Handling
        public void Handle(RegisterResponse message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            //navigationService
            //    .UriFor<MainAppPivotViewModel>()
            //    .WithParam(param => param.Token, message.Token)
            //    .Navigate();
            IsFormAccessible = true;
        }

        public void Handle(ServerErrorMessage message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            MessageBox.Show(message.Code.ToString(), message.Message, MessageBoxButton.OK);
            IsFormAccessible = true;
        }

        public void Handle(bool message)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = message;
        }
        #endregion

        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
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

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyOfPropertyChange(() => Email);
                }
            }
        }

        private string register;
        public string Register
        {
            get { return register; }
            set
            {
                if (register != value)
                {
                    register = value;
                    NotifyOfPropertyChange(() => Register);
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
    }
}
