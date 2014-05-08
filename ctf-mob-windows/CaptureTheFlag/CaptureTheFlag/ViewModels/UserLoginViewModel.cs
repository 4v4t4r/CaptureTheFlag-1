
namespace CaptureTheFlag.ViewModels
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System.Reflection;
    using System.Windows;
    using Windows.Phone.System.Analytics;

    public class UserLoginViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IEventAggregator eventAggregator;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle; //TODO: use requestHandle to abort when neccessary

        public UserLoginViewModel(INavigationService navigationService, ICommunicationService communicationService, IEventAggregator eventAggregator, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.eventAggregator = eventAggregator;
            this.globalStorageService = globalStorageService;

            User = new User();
            
            DisplayName = "Login";

            UsernameTextBlock = "Username:";
            PasswordTextBlock = "Password:";
            LoginButton = "Log me in";

            IsFormAccessible = true;
        }

        #region Actions
        public void LoginAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            requestHandle = communicationService.LoginUser(User, responseAuthenticator =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Communication success callback");
                        globalStorageService.Current.Authenticator = responseAuthenticator;
                        navigationService.UriFor<MainAppPivotViewModel>().Navigate();
                        navigationService.RemoveBackEntry();
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

        private string loginButton;
        public string LoginButton
        {
            get { return loginButton; }
            set
            {
                if (loginButton != value)
                {
                    loginButton = value;
                    NotifyOfPropertyChange(() => LoginButton);
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
