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
        private readonly CommunicationService communicationService;
        private readonly IEventAggregator eventAggregator;
        private readonly GlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle; ///TODO: Use requestHandle to abort when neccessary

        public UserRegistrationViewModel(INavigationService navigationService, CommunicationService communicationService, IEventAggregator eventAggregator, GlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.eventAggregator = eventAggregator;
            this.globalStorageService = globalStorageService;

            User = new User();
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
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Communication success callback");
                        MessageBox.Show("CREATED", String.Format("creater user: {0}", User.Username), MessageBoxButton.OK);
                        //TODO: Returned password sha251 should be removed from server
                        responseUser.Password = null;
                        User = globalStorageService.Current.Users[responseUser.Url] = responseUser;
                        IsFormAccessible = true;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Communication error callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                        IsFormAccessible = true;
                    }
            );
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
                    eventAggregator.Publish(isFormAccessible);
                }
            }
        }
        #endregion
        #endregion
    }
}
