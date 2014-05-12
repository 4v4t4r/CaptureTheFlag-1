using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace CaptureTheFlag.Models
{
    [DataContract]
    public class Item : PropertyChangedBase
    {
        #region Enumerated types
        //TODO: Move Dictionaries to conversion class
        public enum ITEM_TYPE
        {
            RED_FLAG = 3,
            BLUE_FLAG = 4,

            RED_BASE = 5,
            BLUE_BASE = 6,

            RED_BASE_WITH_FLAG = 7,
            BLUE_BASE_WITH_FLAG = 8,

            FIRST_AID_KIT = 9,
            PISTOL = 10,
            AMMO = 11,
        }

        //TODO: Build an inverse, because values are unique
        [JsonIgnore]
        private static Dictionary<string, ITEM_TYPE> itemTypes = new Dictionary<string, ITEM_TYPE> {
            { "Red flag", ITEM_TYPE.RED_FLAG },
            { "Blue flag", ITEM_TYPE.BLUE_FLAG },
            { "Red base", ITEM_TYPE.RED_BASE },
            { "Blue base", ITEM_TYPE.BLUE_BASE },
            { "Red base with flag", ITEM_TYPE.RED_BASE_WITH_FLAG },
            { "Blue base with flag", ITEM_TYPE.BLUE_BASE_WITH_FLAG },
            { "First aid kit", ITEM_TYPE.FIRST_AID_KIT },
            { "Pistol", ITEM_TYPE.PISTOL },
            { "Ammo", ITEM_TYPE.AMMO }
        };

        [JsonIgnore]
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
        #endregion

        #region JSON properties
        [JsonProperty]
        private string url;
        [JsonProperty]
        private string name;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int? type;
        [JsonProperty]
        private Location location;
        [JsonProperty]
        private double? value;
        [JsonProperty]
        private string game;
        #endregion

        #region Object properties
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

        [JsonIgnore]
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

        [JsonIgnore]
        public ITEM_TYPE Type
        {
            get { return (ITEM_TYPE)type; }
            set
            {
                if ((type == null) || ((ITEM_TYPE)type != value))
                {
                    type = (int)value;
                    if (type != null)
                    {
                        Name = ItemTypes.FirstOrDefault(pair => pair.Value == value).Key; //TODO: Change when name is not required, resets user name set
                    }
                    NotifyOfPropertyChange(() => Type);
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
        public double? Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    NotifyOfPropertyChange(() => Value);
                }
            }
        }

        [JsonIgnore]
        public string Game
        {
            get { return game; }
            set
            {
                if (game != value)
                {
                    game = value;
                    NotifyOfPropertyChange(() => Game);
                }
            }
        }
        #endregion
    }
}
