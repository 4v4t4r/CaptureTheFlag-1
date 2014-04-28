namespace CaptureTheFlag.ViewModels
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Windows;

    public class UserRegistrationViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IEventAggregator eventAggregator;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle; ///TODO: Use requestHandle to abort when neccessary

        public UserRegistrationViewModel(INavigationService navigationService, ICommunicationService communicationService, IEventAggregator eventAggregator, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.eventAggregator = eventAggregator;
            this.globalStorageService = globalStorageService;

            User = new User();
            User.device_type = User.DEVICE_TYPE.WP; ///TODO: Do not send device_type information in register request
            IsFormAccessible = true;
            
            DisplayName = "Registration";
            
            UsernameTextBlock = "Username:";
            PasswordTextBlock = "Password:";
            EmailTextBlock = "E-mail:";

            RegisterButton = "Register me";
        }

        #region Actions
        public void RegisterAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            requestHandle = communicationService.RegisterUser(User, responseUser =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("CREATED", String.Format("creater user: {0}", User.username), MessageBoxButton.OK);
                        //TODO: Decide what to do with response User model, note that it containse password sha251
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
        #endregion

        #region ViewModel States
        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Properties

        #region Model Properties
        private User user;
        public User User
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
        #endregion

        #region UI Properties
        private string usernameTextBlock;
        public string UsernameTextBlock
        {
            get { return usernameTextBlock; }
            set
            {
                if (usernameTextBlock != value)
                {
                    usernameTextBlock = value;
                    NotifyOfPropertyChange(() => UsernameTextBlock);
                }
            }
        }

        private string passwordTextBlock;
        public string PasswordTextBlock
        {
            get { return passwordTextBlock; }
            set
            {
                if (passwordTextBlock != value)
                {
                    passwordTextBlock = value;
                    NotifyOfPropertyChange(() => PasswordTextBlock);
                }
            }
        }

        private string emailTextBlock;
        public string EmailTextBlock
        {
            get { return emailTextBlock; }
            set
            {
                if (emailTextBlock != value)
                {
                    emailTextBlock = value;
                    NotifyOfPropertyChange(() => EmailTextBlock);
                }
            }
        }

        private string registerButton;
        public string RegisterButton
        {
            get { return registerButton; }
            set
            {
                if (registerButton != value)
                {
                    registerButton = value;
                    NotifyOfPropertyChange(() => RegisterButton);
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
