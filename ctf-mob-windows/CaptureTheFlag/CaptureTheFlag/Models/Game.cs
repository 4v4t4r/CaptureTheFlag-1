namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    //Reference: https://github.com/blstream/CaptureTheFlag/blob/master/ctf-web-app/docs/models.rst#model-game

    [DataContract]
    public class Game : PropertyChangedBase
    {
        #region Enumerated types
        //TODO: Move Dictionaries to conversion class
        public enum STATUS
        {
            IN_PROGRESS = 0,
            CREATED = 1,
            ON_HOLD = 2,
            CANCELED = 3
        }

        //TODO: Build an inverse, because values are unique
        [IgnoreDataMember]
        private static Dictionary<string, STATUS> statuses = new Dictionary<string, STATUS> {
                { "Canceled", STATUS.CANCELED },
                { "Created", STATUS.CREATED },
                { "In progress", STATUS.IN_PROGRESS },
                { "On hold", STATUS.ON_HOLD }
        };

        [IgnoreDataMember]
        public Dictionary<string, STATUS> Statuses
        {
            get { return statuses; }
            set
            {
                if (statuses != value)
                {
                    statuses = value;
                    NotifyOfPropertyChange(() => Statuses);
                }
            }
        }

        public enum TYPE
        {
            FRAGS = 0,
            TIME = 1
        }

        //TODO: Build an inverse, because values are unique
        [IgnoreDataMember]
        private static Dictionary<string, TYPE> types = new Dictionary<string, TYPE> {
                { "Frags", TYPE.FRAGS },
                { "Time", TYPE.TIME }
        };

        [IgnoreDataMember]
        public Dictionary<string, TYPE> Types
        {
            get { return types; }
            set
            {
                if (types != value)
                {
                    types = value;
                    NotifyOfPropertyChange(() => Types);
                }
            }
        }
        #endregion

        //TODO: research and consider ignoring numerical values when serializing
        #region JSON properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string url;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string start_time;
        [JsonProperty]
        private int max_players;
        [JsonProperty]
        private int status;
        [JsonProperty]
        private int type;
        [JsonProperty]
        private double radius;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private Location location;
        [JsonProperty]
        private double visibility_range;
        [JsonProperty]
        private double action_range;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private BindableCollection<string> players;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private BindableCollection<string> invited_users;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string owner;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string last_modified;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string created;
        #endregion

        #region Model properties
        [IgnoreDataMember]
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

        [IgnoreDataMember]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        [IgnoreDataMember]
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyOfPropertyChange(() => Description);
                }
            }
        }

        [IgnoreDataMember]
        public DateTime StartTime
        {
            //TODO: Add error/Exception handling
            get
            {
                if (start_time == null)
                {
                    //TODO: Check if it works
                    return DateTime.Now.AddHours(1.0);
                }
                return DateTime.ParseExact(start_time, "s", null);
            }
            set
            {
                start_time = value.ToString("s");
                NotifyOfPropertyChange(() => StartTime);
            }
        }

        [IgnoreDataMember]
        public int MaxPlayers
        {
            get { return max_players; }
            set
            {
                if (max_players != value)
                {
                    max_players = value;
                    NotifyOfPropertyChange(() => MaxPlayers);
                }
            }
        }

        [IgnoreDataMember]
        public STATUS Status
        {
            get { return (STATUS)status; }
            set
            {
                if ((STATUS)status != value)
                {
                    status = (int)value;
                    NotifyOfPropertyChange(() => Status);
                }
            }
        }

        [IgnoreDataMember]
        public TYPE Type
        {
            get { return (TYPE)type; }
            set
            {
                if ((TYPE)type != value)
                {
                    type = (int)value;
                    NotifyOfPropertyChange(() => Type);
                }
            }
        }

        [IgnoreDataMember]
        public double Radius
        {
            get { return radius; }
            set
            {
                if (radius != value)
                {
                    radius = value;
                    NotifyOfPropertyChange(() => Radius);
                }
            }
        }

        [IgnoreDataMember]
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

        [IgnoreDataMember]
        public double VisibilityRange
        {
            get { return visibility_range; }
            set
            {
                if (visibility_range != value)
                {
                    visibility_range = value;
                    NotifyOfPropertyChange(() => VisibilityRange);
                }
            }
        }

        [IgnoreDataMember]
        public double ActionRange
        {
            get { return action_range; }
            set
            {
                if (action_range != value)
                {
                    action_range = value;
                    NotifyOfPropertyChange(() => ActionRange);
                }
            }
        }

        [IgnoreDataMember]
        public BindableCollection<string> Players
        {
            get { return players; }
            set
            {
                if (players != value)
                {
                    players = value;
                    NotifyOfPropertyChange(() => Players);
                }
            }
        }

        [IgnoreDataMember]
        public BindableCollection<string> InvitedUsers
        {
            get { return invited_users; }
            set
            {
                if (invited_users != value)
                {
                    invited_users = value;
                    NotifyOfPropertyChange(() => InvitedUsers);
                }
            }
        }

        [IgnoreDataMember]
        public string Owner
        {
            get { return owner; }
            set
            {
                if (owner != value)
                {
                    owner = value;
                    NotifyOfPropertyChange(() => Owner);
                }
            }
        }

        [IgnoreDataMember]
        public DateTime LastModified
        {
            //TODO: Add error/Exception handling
            get
            {
                return DateTime.ParseExact(last_modified, "s", null);
            }
            set
            {
                last_modified = value.ToString("s");
                NotifyOfPropertyChange(() => LastModified);
            }
        }

        [IgnoreDataMember]
        public DateTime Created
        {
            //TODO: Add error/Exception handling
            get
            {
                return DateTime.ParseExact(created, "s", null);
            }
            set
            {
                created = value.ToString("s");
                NotifyOfPropertyChange(() => Created);
            }
        }
        #endregion
    }

    //Game Model:
    /*{
        url: string # url for current resource
        name: string # (required=True, max_length=100)
        description: string # (null=True, blank=True, max_length=255)
        start_time: date_time
        max_players: int
        status: int # (choices=GAME_STATUSES)
        type: int # (choices=GAME_TYPE)
        radius: float # in meters
        location: {
            lat: float,
            lon: float
        }
        visibility_range: float
        action_range: float
        players: [] # urls for players objects (object Character)
        invited_users: [] # urls for invited users objects (object PortalUser)
        owner: string # read_only=True, url for user
        last_modified: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
        created: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
    }*/

    //Game Model JSON:
    /*{
        "url": "string",
        "name": "string",
        "description": "string",
        "start_time": "date_time",
        "max_players" : 0,
        "status": 0,
        "type": 0,
        "radius": 1.0,
        "location": {
            "lat": 1.0,
            "lon": 1.0
        },
        "visibility_range": 1.0,
        "action_range": 1.0,
        "players": [ "string", "string" ],
        "invited_users": [ "string", "string" ],
        "owner": "string",
        "last_modified": "date_time",
        "created": "date_time"
    }*/
}
