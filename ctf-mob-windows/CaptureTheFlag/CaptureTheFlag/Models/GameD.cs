using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    //    {
    //    url: string # url for current resource
    //    name: string # (required=True, max_length=100)
    //    description: string # (null=True, blank=True, max_length=255)
    //    start_time: date_time
    //    max_players: int
    //    status: int # (choices=GAME_STATUSES)
    //    type: int # (choices=GAME_TYPE)
    //    map: string # url for map object
    //    visibility_range: float
    //    action_range: float
    //    players: [] # urls for players objects (object Character)
    //    invited_users: [] # urls for invited users objects (object PortalUser)
    //}

    public class GameD : PropertyChangedBase
    {


        private string _url; //url for current resource
        private string _name; //(required=True, max_length=100)
        private string _description; //(null=True, blank=True, max_length=255)
        private string _start_time; //date_time
        private int _max_players;
        private int _status; //(choices=GAME_STATUSES)
        private int _type; //(choices=GAME_TYPE)
        private string _map; //url for map object
        private float _visibility_range;
        private float _action_range;
        public List<string> players { get; set; } //urls for players objects (object Character)
        public List<object> invited_users { get; set; } //urls for invited users objects (object PortalUser)
        public List<string> items { get; set; }

        #region Enumerated types
        public enum STATUSES
        {
            IN_PROGRESS = 0,
            CREATED = 1,
            ON_HOLD = 2,
            CANCELED = 3
        }

        private static Dictionary<string, STATUSES> statuses = new Dictionary<string, STATUSES> {
                { "Canceled", STATUSES.CANCELED },
                { "Created", STATUSES.CREATED },
                { "In progress", STATUSES.IN_PROGRESS },
                { "On hold", STATUSES.ON_HOLD }
        };

        public enum GAME_TYPE
        {
            FRAGS = 0,
            TIME = 1
        }

        private static Dictionary<string, GAME_TYPE> gameTypes = new Dictionary<string, GAME_TYPE> {
                { "Frags", GAME_TYPE.FRAGS },
                { "Time", GAME_TYPE.TIME }
        };
        #endregion

        #region Observable Properties
        
        #region Dummy Properties

        public string starttime
        {
            get { return _start_time; }
            set
            {
                if (_start_time != value)
                {
                    _start_time = value;
                    NotifyOfPropertyChange(() => starttime);
                    NotifyOfPropertyChange(() => start_time);
                }
            }
        }

        public int maxplayers
        {
            get { return _max_players; }
            set
            {
                if (_max_players != value)
                {
                    _max_players = value;
                    NotifyOfPropertyChange(() => maxplayers);
                    NotifyOfPropertyChange(() => max_players);
                }
            }
        }

        public float visibilityrange
        {
            get { return _visibility_range; }
            set
            {
                if (_visibility_range != value)
                {
                    _visibility_range = value;
                    NotifyOfPropertyChange(() => visibilityrange);
                    NotifyOfPropertyChange(() => visibility_range);
                }
            }
        }

        public float actionrange
        {
            get { return _action_range; }
            set
            {
                if (_action_range != value)
                {
                    _action_range = value;
                    NotifyOfPropertyChange(() => actionrange);
                    NotifyOfPropertyChange(() => action_range);
                }
            }
        }

        //TODO: make a converter
        public Dictionary<string, GAME_TYPE> GameTypes
        {
            get { return gameTypes; }
            set
            {
                if (gameTypes != value)
                {
                    gameTypes = value;
                    NotifyOfPropertyChange(() => GameTypes);
                }
            }
        }

        private string selectedGameType;
        public string SelectedGameType
        {
            get { return selectedGameType; }
            set
            {
                selectedGameType = value;
                type = (int)GameTypes[selectedGameType];
                NotifyOfPropertyChange(() => type);
                NotifyOfPropertyChange(() => SelectedGameType);
            }
        }
        #endregion

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

        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(() => name);
                }
            }
        }

        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyOfPropertyChange(() => description);
                }
            }
        }

        public string start_time
        {
            get { return _start_time; }
            set
            {
                if (_start_time != value)
                {
                    _start_time = value;
                    NotifyOfPropertyChange(() => starttime);
                    NotifyOfPropertyChange(() => start_time);
                }
            }
        }

        public int max_players
        {
            get { return _max_players; }
            set
            {
                if (_max_players != value)
                {
                    _max_players = value;
                    NotifyOfPropertyChange(() => maxplayers);
                    NotifyOfPropertyChange(() => max_players);
                }
            }
        }

        public int status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    NotifyOfPropertyChange(() => status);
                }
            }
        }

        public int type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    selectedGameType = GameTypes.FirstOrDefault(e => e.Value == (GAME_TYPE)_type).Key;
                    NotifyOfPropertyChange(() => type);
                    NotifyOfPropertyChange(() => SelectedGameType);
                }
            }
        }

        public string map
        {
            get { return _map; }
            set
            {
                if (_map != value)
                {
                    _map = value;
                    NotifyOfPropertyChange(() => map);
                }
            }
        }

        public float visibility_range
        {
            get { return _visibility_range; }
            set
            {
                if (_visibility_range != value)
                {
                    _visibility_range = value;
                    NotifyOfPropertyChange(() => visibilityrange);
                    NotifyOfPropertyChange(() => visibility_range);
                }
            }
        }

        public float action_range
        {
            get { return _action_range; }
            set
            {
                if (_action_range != value)
                {
                    _action_range = value;
                    NotifyOfPropertyChange(() => actionrange);
                    NotifyOfPropertyChange(() => action_range);
                }
            }
        }
        #endregion
    }
}
