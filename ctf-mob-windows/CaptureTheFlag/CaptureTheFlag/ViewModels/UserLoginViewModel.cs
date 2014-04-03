using Caliburn.Micro;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserLoginViewModel : Screen
    {
        private string username;
        private string password;
        private string login;

        public UserLoginViewModel()
        {
            DisplayName = "User Login";
            Login = "Login";
        }

        #region Actions
        public void LoginAction()
        {
            MessageBox.Show("Logged in");
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
