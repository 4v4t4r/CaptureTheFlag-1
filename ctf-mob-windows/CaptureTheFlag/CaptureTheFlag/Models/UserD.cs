namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using System.Collections.Generic;

    public class UserD : PropertyChangedBase
    {
        #region Enumerated types
        //Device types - mapping:
        //DEVICE_TYPES = {
        //    0 - 'ANDROID',
        //    1 - 'WP',
        //    2 - 'IOS'
        //}

        public enum DEVICE_TYPE
        {
            ANDROID = 0,
            WP = 1,
            IOS = 2
        }
        //public static class DEVICE_TYPE
        //{
        //    public static string ANDROID { get { return "android"; } private set { } }
        //    public static string WP { get { return "wp"; } private set { } }
        //    public static string IOS { get { return "ios"; } private set { } }
        //}


        private static Dictionary<string, DEVICE_TYPE> deviceTypes = new Dictionary<string, DEVICE_TYPE> {
                { "Android", DEVICE_TYPE.ANDROID },
                { "Windows Phone", DEVICE_TYPE.WP },
                { "iOS", DEVICE_TYPE.IOS }
        };

        //private static Dictionary<string, string> deviceTypes = new Dictionary<string, string> {
        //        { "Android", "android" },
        //        { "Windows Phone", "wp" },
        //        { "iOS", "ios" }
        //};
        #endregion


        //{
        //url: string # url to current resource
        //username: string # (required=True, max_length=50, unique=True)
        //email: string # (required=True, max_length=50)
        //password: string # (required=True, min_length=6, max_length=50)
        //nick: string # (required=False, max_length=100)
        //device_type: int (required=False)
        //device_id: string (required=False, max_length=255)
        //lat: float # (required=False)
        //lon: float # (required=False)
        //characters = [ ] # list of url for characters objects
        //}

        private string _url;
        private string _username;
        private string _password;
        private string _first_name;
        private string _last_name;
        private string _email;
        private string _nick;
        private string _active_character; //TODO...
        private List<string> _characters; //TODO: change to BindableCollection when implemented custom json deserializer
        private int _device_type;
        //private string _device_type;
        private string _device_id;
        private Location _location; //TODO...

        #region Dummy Properties
        public string firstname
        {
            get { return _first_name; }
            set
            {
                if (_first_name != value)
                {
                    _first_name = value;
                    NotifyOfPropertyChange(() => first_name);
                    NotifyOfPropertyChange(() => firstname);
                }
            }
        }

        public string lastname
        {
            get { return _last_name; }
            set
            {
                if (_last_name != value)
                {
                    _last_name = value;
                    NotifyOfPropertyChange(() => last_name);
                    NotifyOfPropertyChange(() => lastname);
                }
            }
        }

        public string activecharacter
        {
            get { return _active_character; }
            set
            {
                if (_active_character != value)
                {
                    _active_character = value;
                    NotifyOfPropertyChange(() => active_character);
                    NotifyOfPropertyChange(() => activecharacter);
                }
            }
        }

        //TODO: make a converter
        public Dictionary<string, DEVICE_TYPE> DeviceTypes
        {
            get { return deviceTypes; }
            set
            {
                if (deviceTypes != value)
                {
                    deviceTypes = value;
                    NotifyOfPropertyChange(() => DeviceTypes);
                }
            }
        }

        //public Dictionary<string, string> DeviceTypes
        //{
        //    get { return deviceTypes; }
        //    set
        //    {
        //        if (deviceTypes != value)
        //        {
        //            deviceTypes = value;
        //            NotifyOfPropertyChange(() => DeviceTypes);
        //        }
        //    }
        //}

        private string selectedDeviceType;
        public string SelectedDeviceType
        {
            get { return selectedDeviceType; }
            set
            {
                selectedDeviceType = value;
                _device_type = (int)DeviceTypes[selectedDeviceType];
                //_device_type = DeviceTypes[selectedDeviceType];
                NotifyOfPropertyChange(() => device_type);
                NotifyOfPropertyChange(() => devicetype);
                NotifyOfPropertyChange(() => SelectedDeviceType);
            }
        }
        public int devicetype
        {
            get { return _device_type; }
            set
            {
                if (_device_type != value)
                {
                    _device_type = value;
                    NotifyOfPropertyChange(() => device_type);
                    NotifyOfPropertyChange(() => devicetype);
                    NotifyOfPropertyChange(() => SelectedDeviceType);
                }
            }
        }

        //public string devicetype
        //{
        //    get { return _device_type; }
        //    set
        //    {
        //        if (_device_type != value)
        //        {
        //            _device_type = value;
        //            NotifyOfPropertyChange(() => device_type);
        //            NotifyOfPropertyChange(() => devicetype);
        //            NotifyOfPropertyChange(() => SelectedDeviceType);
        //        }
        //    }
        //}

        public string deviceid
        {
            get { return _device_id; }
            set
            {
                if (_device_id != value)
                {
                    _device_id = value;
                    NotifyOfPropertyChange(() => device_id);
                    NotifyOfPropertyChange(() => deviceid);
                }
            }
        }
        #endregion

        #region Observable Properties
        public string url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    NotifyOfPropertyChange(() => url);
                }
            }
        }

        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyOfPropertyChange(() => username);
                }
            }
        }
        public string password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyOfPropertyChange(() => password);
                }
            }
        }

        public string first_name
        {
            get { return _first_name; }
            set
            {
                if (_first_name != value)
                {
                    _first_name = value;
                    NotifyOfPropertyChange(() => first_name);
                    NotifyOfPropertyChange(() => firstname);
                }
            }
        }

        public string last_name
        {
            get { return _last_name; }
            set
            {
                if (_last_name != value)
                {
                    _last_name = value;
                    NotifyOfPropertyChange(() => last_name);
                    NotifyOfPropertyChange(() => lastname);
                }
            }
        }

        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyOfPropertyChange(() => email);
                }
            }
        }

        public string nick
        {
            get { return _nick; }
            set
            {
                if (_nick != value)
                {
                    _nick = value;
                    NotifyOfPropertyChange(() => nick);
                }
            }
        }

        public string active_character
        {
            get { return _active_character; }
            set
            {
                if (_active_character != value)
                {
                    _active_character = value;
                    NotifyOfPropertyChange(() => active_character);
                    NotifyOfPropertyChange(() => activecharacter);
                }
            }
        }

        public int device_type
        {
            get { return _device_type; }
            set
            {
                if (_device_type != value)
                {
                    _device_type = value;
                    NotifyOfPropertyChange(() => device_type);
                    NotifyOfPropertyChange(() => devicetype);
                    NotifyOfPropertyChange(() => SelectedDeviceType);
                }
            }
        }

        //public string device_type
        //{
        //    get { return _device_type; }
        //    set
        //    {
        //        if (_device_type != value)
        //        {
        //            _device_type = value;
        //            NotifyOfPropertyChange(() => device_type);
        //            NotifyOfPropertyChange(() => devicetype);
        //            NotifyOfPropertyChange(() => SelectedDeviceType);
        //        }
        //    }
        //}

        public string device_id
        {
            get { return _device_id; }
            set
            {
                if (_device_id != value)
                {
                    _device_id = value;
                    NotifyOfPropertyChange(() => device_id);
                    NotifyOfPropertyChange(() => deviceid);
                }
            }
        }


        public Location location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    NotifyOfPropertyChange(() => location);
                }
            }
        }

        public List<string> characters
        {
            get { return _characters; }
            set
            {
                if (_characters != value)
                {
                    _characters = value;
                    NotifyOfPropertyChange(() => characters);
                }
            }
        }
        #endregion

    }
}
