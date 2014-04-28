namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;

    public class Authenticator : PropertyChangedBase
    {

        public static bool IsValid(Authenticator authenticator)
        {
            return authenticator != null && !string.IsNullOrEmpty(authenticator.user) && !string.IsNullOrEmpty(authenticator.token);
        }

        private string _token;
        private string _user;

        public string token
        {
            get { return _token; }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    NotifyOfPropertyChange(() => token);
                }
            }
        }

        public string user
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    NotifyOfPropertyChange(() => user);
                }
            }
        }
    }
}
