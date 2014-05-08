using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public static class Extensions
    {
        private static Dictionary<GameD.STATUSES, string> statusNames;
        private static Dictionary<GameD.GAME_TYPE, string> gameTypeNames;
        static Extensions()
        {
            statusNames = new Dictionary<GameD.STATUSES, string>();
            statusNames.Add(GameD.STATUSES.IN_PROGRESS, "In progress");
            statusNames.Add(GameD.STATUSES.CREATED, "Created");
            statusNames.Add(GameD.STATUSES.ON_HOLD, "On hold");
            statusNames.Add(GameD.STATUSES.CANCELED, "Canceled");

            gameTypeNames = new Dictionary<GameD.GAME_TYPE, string>();
            gameTypeNames.Add(GameD.GAME_TYPE.FRAGS, "Frags");
            gameTypeNames.Add(GameD.GAME_TYPE.TIME, "Time");
        }

        public static string GetName(this GameD.STATUSES status)
        {
            return statusNames[status];
        }

        public static string GetName(this GameD.GAME_TYPE gameType)
        {
            return gameTypeNames[gameType];
        }

        public static string Name { get { return statusNames[GameD.STATUSES.IN_PROGRESS]; } private set { } }
    }
}
