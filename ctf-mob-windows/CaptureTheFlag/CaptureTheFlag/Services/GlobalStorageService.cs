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
    public class GameCache : Dictionary<string, Game>
    {
        public Game GetFromCache(string key)
        {
            if (ContainsKey(key))
                return this[key];

            return null;
        }
    }

    public class GlobalStorageService
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

        private GameCache _cachedGames;
        public GameCache Games
        {
            get
            {
                if (_cachedGames == null) _cachedGames = new GameCache();
                return _cachedGames;
            }
        }
    }
}
