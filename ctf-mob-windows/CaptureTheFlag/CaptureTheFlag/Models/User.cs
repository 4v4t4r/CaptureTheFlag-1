namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    //Reference: https://github.com/blstream/CaptureTheFlag/blob/master/ctf-web-app/docs/models.rst#model-user

    [DataContract]
    public class User : PropertyChangedBase
    {
        #region Enumerated types
        //TODO: Move Dictionaries to conversion class
        public enum DEVICE_TYPE
        {
            ANDROID = 0,
            WP = 1,
            IOS = 2
        }

        //TODO: Build an inverse, because values are unique
        [JsonIgnore]
        private static Dictionary<string, DEVICE_TYPE> deviceTypes = new Dictionary<string, DEVICE_TYPE> {
                { "Android", DEVICE_TYPE.ANDROID },
                { "Windows Phone", DEVICE_TYPE.WP },
                { "iOS", DEVICE_TYPE.IOS }
        };

        [JsonIgnore]
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

        public enum TEAM_TYPE
        {
            RED_TEAM = 0,
            BLUE_TEAM = 1
        }

        //TODO: Build an inverse, because values are unique
        [JsonIgnore]
        private static Dictionary<string, TEAM_TYPE> teamTypes = new Dictionary<string, TEAM_TYPE> {
                { "Red team", TEAM_TYPE.RED_TEAM },
                { "Blue team", TEAM_TYPE.BLUE_TEAM }
        };

        [JsonIgnore]
        public Dictionary<string, TEAM_TYPE> TeamTypes
        {
            get { return teamTypes; }
            set
            {
                if (teamTypes != value)
                {
                    teamTypes = value;
                    NotifyOfPropertyChange(() => TeamTypes);
                }
            }
        }
        #endregion

        //TODO: research and consider ignoring numerical values when serializing
        #region JSON properties
        [JsonProperty]
        private string url;
        [JsonProperty]
        private string username;
        [JsonProperty]
        private string email;
        [JsonProperty]
        private string password;
        [JsonProperty]
        private string nick;
        [JsonProperty]
        private int? device_type;
        [JsonProperty]
        private string device_id;
        [JsonProperty]
        private Location location;
        [JsonProperty]
        private int? team;
        [JsonProperty]
        private BindableCollection<string> characters;
        #endregion

        #region Model properties
        [JsonIgnore]
        public string Url
        {
            get { return url; }
            set
            {
                if (url != value)
                {
                    url = value;
                    NotifyOfPropertyChange(() => Url);
                }
            }
        }

        [JsonIgnore]
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

        [JsonIgnore]
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

        [JsonIgnore]
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

        [JsonIgnore]
        public string Nick
        {
            get { return nick; }
            set
            {
                if (nick != value)
                {
                    nick = value;
                    NotifyOfPropertyChange(() => Nick);
                }
            }
        }

        [JsonIgnore]
        public DEVICE_TYPE DeviceType
        {
            get
            {
                return (DEVICE_TYPE)device_type;
            }
            set
            {
                if ((device_type == null) || ((DEVICE_TYPE)device_type != value))
                {
                    device_type = (int)value;
                    NotifyOfPropertyChange(() => DeviceType);
                }
            }
        }

        [JsonIgnore]
        public string DeviceId
        {
            get { return device_id; }
            set
            {
                if (device_id != value)
                {
                    device_id = value;
                    NotifyOfPropertyChange(() => DeviceId);
                }
            }
        }

        [JsonIgnore]
        public Location Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    NotifyOfPropertyChange(() => Location);
                }
            }
        }

        [JsonIgnore]
        public TEAM_TYPE Team
        {
            get { return (TEAM_TYPE)team; }
            set
            {
                if ((TEAM_TYPE)team != value)
                {
                    team = (int)value;
                    NotifyOfPropertyChange(() => Team);
                }
            }
        }

        [JsonIgnore]
        public BindableCollection<string> Characters
        {
            get { return characters; }
            set
            {
                if (characters != value)
                {
                    characters = value;
                    NotifyOfPropertyChange(() => Characters);
                }
            }
        }
        #endregion
    }
}
