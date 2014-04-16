using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public class GameItem : PropertyChangedBase
    {
        #region Enumerated types
        //ITEM TYPES - MAPPING: {
        //0 - 'FLAG_RED',
        //1 - 'FLAG_BLUE',
        //2 - 'BASE_RED',
        //3 - 'BASE_BLUE',
        //4 - 'AID_KIT',
        //5 - 'PISTOL',
        //6 - 'AMMO'
        //}
        public enum ITEM_TYPE
        {
            FLAG_RED = 0,
            FLAG_BLUE = 1,
            BASE_RED = 2,
            BASE_BLUE = 3,
            AID_KIT = 4,
            PISTOL = 5,
            AMMO = 6
        }

        private static Dictionary<string, ITEM_TYPE> itemTypes = new Dictionary<string, ITEM_TYPE> {
                { "Red flag", ITEM_TYPE.FLAG_RED },
                { "Blue flag", ITEM_TYPE.FLAG_BLUE },
                { "Red base", ITEM_TYPE.BASE_RED },
                { "Blue base", ITEM_TYPE.BASE_BLUE },
                { "Aid kit", ITEM_TYPE.AID_KIT },
                { "Pistol", ITEM_TYPE.PISTOL },
                { "Ammo", ITEM_TYPE.AMMO }
        };
        #endregion

        //{
        //url: string # url of current resource
        //name: string
        //type: int # item type
        //value: float
        //description: string # (null=True, max_length=255)
        //lat: float
        //lon: float
        //game: string # url for game object
        //}

        private string _url;
        private string _name;
        private int _type;
        private float _value;
        private string _description;
        private float _lat;
        private float _lon;
        private string _game;

        #region Dummy Properties
        //TODO: make a converter
        public Dictionary<string, ITEM_TYPE> ItemTypes
        {
            get { return itemTypes; }
            set
            {
                if (itemTypes != value)
                {
                    itemTypes = value;
                    NotifyOfPropertyChange(() => ItemTypes);
                }
            }
        }

        private string selectedItemType;
        public string SelectedItemType
        {
            get { return selectedItemType; }
            set
            {
                selectedItemType = value;
                type = (int)ItemTypes[selectedItemType];
                NotifyOfPropertyChange(() => type);
                NotifyOfPropertyChange(() => SelectedItemType);
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

        public int type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    selectedItemType = ItemTypes.FirstOrDefault(e => e.Value == (ITEM_TYPE)_type).Key;
                    NotifyOfPropertyChange(() => type);
                    NotifyOfPropertyChange(() => SelectedItemType);
                }
            }
        }

        public float value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyOfPropertyChange(() => this.value);
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

        public float lat
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                {
                    _lat = value;
                    NotifyOfPropertyChange(() => lat);
                }
            }
        }

        public float lon
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                {
                    _lon = value;
                    NotifyOfPropertyChange(() => lon);
                }
            }
        }

        public string game
        {
            get { return _game; }
            set
            {
                if (_game != value)
                {
                    _game = value;
                    NotifyOfPropertyChange(() => game);
                }
            }
        }
        #endregion
    }
}
