using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public class Character : PropertyChangedBase
    {

        #region Enumerated types
        //CHARACTER_TYPES = [
        //0 - 'Private',
        //1 - 'Medic',
        //2 - 'Commandos',
        //3 - 'Spy'
        //]

        public enum CHARACTER_TYPE
        {
            PRIVATE = 0,
            MEDIC = 1,
            COMMANDO = 2,
            SPY = 3
        }

        private static Dictionary<string, CHARACTER_TYPE> characterTypes = new Dictionary<string, CHARACTER_TYPE> {
                { "Private", CHARACTER_TYPE.PRIVATE },
                { "Medic", CHARACTER_TYPE.MEDIC },
                { "Commando", CHARACTER_TYPE.COMMANDO },
                { "Spy", CHARACTER_TYPE.SPY },
        };
        #endregion

        //{
        //url: string # url to current resource
        //user: string # url for user object
        //type: int # (choices=CHARACTER_TYPES)
        //total_time: int
        //total_score: int
        //health: float
        //level: int
        //is_active: boolean
        //}

        private string _url;
        private string _user;
        private int _type;
        private int _total_time;
        private int _total_score;
        private float _health;
        private int _level;
        private bool _is_active;

        #region Dummy Properties
        //TODO: make a converter
        public Dictionary<string, CHARACTER_TYPE> CharacterTypes
        {
            get { return characterTypes; }
            set
            {
                if (characterTypes != value)
                {
                    characterTypes = value;
                    NotifyOfPropertyChange(() => CharacterTypes);
                }
            }
        }

        private string selectedCharacterType;
        public string SelectedCharacterType
        {
            get { return selectedCharacterType; }
            set
            {
                selectedCharacterType = value;
                type = (int)CharacterTypes[selectedCharacterType];
                NotifyOfPropertyChange(() => type);
                NotifyOfPropertyChange(() => SelectedCharacterType);
            }
        }

        public int totaltime
        {
            get { return _total_time; }
            set
            {
                if (_total_time != value)
                {
                    _total_time = value;
                    NotifyOfPropertyChange(() => total_time);
                    NotifyOfPropertyChange(() => totaltime);
                }
            }
        }

        public int totalscore
        {
            get { return _total_score; }
            set
            {
                if (_total_score != value)
                {
                    _total_score = value;
                    NotifyOfPropertyChange(() => total_score);
                    NotifyOfPropertyChange(() => totalscore);
                }
            }
        }

        public bool isactive
        {
            get { return _is_active; }
            set
            {
                if (_is_active != value)
                {
                    _is_active = value;
                    NotifyOfPropertyChange(() => is_active);
                    NotifyOfPropertyChange(() => isactive);
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

        public int type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    selectedCharacterType = CharacterTypes.FirstOrDefault(e => e.Value == (CHARACTER_TYPE)_type).Key;
                    NotifyOfPropertyChange(() => type);
                    NotifyOfPropertyChange(() => SelectedCharacterType);
                }
            }
        }

        public int total_time
        {
            get { return _total_time; }
            set
            {
                if (_total_time != value)
                {
                    _total_time = value;
                    NotifyOfPropertyChange(() => total_time);
                    NotifyOfPropertyChange(() => totaltime);
                }
            }
        }

        public int total_score
        {
            get { return _total_score; }
            set
            {
                if (_total_score != value)
                {
                    _total_score = value;
                    NotifyOfPropertyChange(() => total_score);
                    NotifyOfPropertyChange(() => totalscore);
                }
            }
        }

        public float health
        {
            get { return _health; }
            set
            {
                if (_health != value)
                {
                    _health = value;
                    NotifyOfPropertyChange(() => health);
                }
            }
        }

        public int level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    NotifyOfPropertyChange(() => level);
                }
            }
        }

        public bool is_active
        {
            get { return _is_active; }
            set
            {
                if (_is_active != value)
                {
                    _is_active = value;
                    NotifyOfPropertyChange(() => is_active);
                    NotifyOfPropertyChange(() => isactive);
                }
            }
        }
        #endregion
    }
}
