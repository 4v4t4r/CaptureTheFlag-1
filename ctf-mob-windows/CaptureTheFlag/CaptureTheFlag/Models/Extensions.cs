using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public static class Extensions
    {
        private static Dictionary<Game.STATUSES, string> statusNames;
        private static Dictionary<Game.GAME_TYPE, string> gameTypeNames;
        static Extensions()
        {
            statusNames = new Dictionary<Game.STATUSES, string>();
            statusNames.Add(Game.STATUSES.IN_PROGRESS, "In progress");
            statusNames.Add(Game.STATUSES.CREATED, "Created");
            statusNames.Add(Game.STATUSES.ON_HOLD, "On hold");
            statusNames.Add(Game.STATUSES.CANCELED, "Canceled");

            gameTypeNames = new Dictionary<Game.GAME_TYPE, string>();
            gameTypeNames.Add(Game.GAME_TYPE.FRAGS, "Frags");
            gameTypeNames.Add(Game.GAME_TYPE.TIME, "Time");
        }

        public static string GetName(this Game.STATUSES status)
        {
            return statusNames[status];
        }

        public static string GetName(this Game.GAME_TYPE gameType)
        {
            return gameTypeNames[gameType];
        }

        public static string Name { get { return statusNames[Game.STATUSES.IN_PROGRESS]; } private set { } }
    }
}
