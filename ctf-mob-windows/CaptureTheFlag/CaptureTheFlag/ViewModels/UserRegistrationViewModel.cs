using Caliburn.Micro;
using System.Windows;
using CaptureTheFlag.Services;

namespace CaptureTheFlag.ViewModels
{
    public class UserRegistrationViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private string username;
        private string password;
        private string email;
        private string register;

        public UserRegistrationViewModel(INavigationService navigationService, ICommunicationService communicationService)
        {
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            DisplayName = "Registration";
            Register = "Register";
        }

        #region Actions
        public void RegisterAction()
        {
            communicationService.Register("sdf", "sdf", "sdf");

            //navigationService
            //    .UriFor<MainAppPivotViewModel>()
            //    .WithParam(param => param.Name, "Register")
            //    .Navigate();
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
