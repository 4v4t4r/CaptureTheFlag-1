using Caliburn.Micro;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserLoginViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private string username;
        private string password;
        private string login;

        public UserLoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            DisplayName = "Login";
            Login = "Login";
        }

        #region Actions
        public void LoginAction()
        {
            navigationService
                .UriFor<MainAppPivotViewModel>()
                .WithParam(param => param.Name, "Login")
                .Navigate();
        }
        #endregion

        #region Properties
        public string Username
        { 
            get { return username; }
            set
            {
                if(username != value)
                {
                    username = value;
                    NotifyOfPropertyChange("Username");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyOfPropertyChange("Password");
                }
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    NotifyOfPropertyChange("Login");
                }
            }
        }
#endregion
    }
}
