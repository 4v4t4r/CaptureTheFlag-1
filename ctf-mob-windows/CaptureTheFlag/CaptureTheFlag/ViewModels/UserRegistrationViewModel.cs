using Caliburn.Micro;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserRegistrationViewModel : Screen
    {
        private string username;
        private string password;
        private string email;
        private string register;

        public UserRegistrationViewModel()
        {
            DisplayName = "User registration";
            Register = "Register";
        }

        #region Actions
        public void RegisterAction()
        {
            MessageBox.Show("Registered");
        }
        #endregion

        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
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

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyOfPropertyChange("Email");
                }
            }
        }

        public string Register
        {
            get { return register; }
            set
            {
                if (register != value)
                {
                    register = value;
                    NotifyOfPropertyChange("Register");
                }
            }
        }
        #endregion
    }
}
