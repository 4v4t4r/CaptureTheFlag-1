using Caliburn.Micro;
using CaptureTheFlag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    //Reference: http://www.matthidinger.com/archive/2011/12/04/RealWorldWPDev-Part-6-Page-Navigation-and-passing-Complex-State.aspx
    //TODO: Change/modify if approach is desirable

    public class GameCache : Dictionary<string, PreGame>
    {
        public PreGame GetFromCache(string key)
        {
            if (ContainsKey(key))
                return this[key];

            return null;
        }
    }

    public class GameMapCache : Dictionary<string, GameMap>
    {
        public GameMap GetFromCache(string key)
        {
            if (ContainsKey(key))
                return this[key];

            return null;
        }
    }

    public class UserCache : Dictionary<string, User>
    {
        public User GetFromCache(string key)
        {
            if (ContainsKey(key))
                return this[key];

            return null;
        }
    }

    public class GlobalStorageService : IGlobalStorageService
    {
        private GlobalStorageService _current;
        public GlobalStorageService Current
        {
            get
            {
                if (_current == null) _current = new GlobalStorageService();
                return _current;
            }
            set { _current = value; }
        }

        private GameMapCache _cachedGameMaps;
        public GameMapCache GameMaps
        {
            get
            {
                if (_cachedGameMaps == null) _cachedGameMaps = new GameMapCache();
                return _cachedGameMaps;
            }
        }

        private GameCache _cachedGames;
        public GameCache Games
        {
            get
            {
                if (_cachedGames == null) _cachedGames = new GameCache();
                return _cachedGames;
            }
        }

        private UserCache _cachedUsers;
        public UserCache Users
        {
            get
            {
                if (_cachedUsers == null) _cachedUsers = new UserCache();
                return _cachedUsers;
            }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                if (token != value)
                {
                    token = value;
                }
            }
        }

        private BindableCollection<Item> items;
        public BindableCollection<Item> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                }
            }
        }

        private Authenticator authenticator;
        public Authenticator Authenticator
        {
            get { return authenticator; }
            set
            {
                if (authenticator != value)
                {
                    authenticator = value;
                }
            }
        }
    }
}
