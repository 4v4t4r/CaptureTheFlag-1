namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using Newtonsoft.Json;

    public class GameStatus : PropertyChangedBase
    {
        #region JSON properties
        [JsonProperty]
        private int red_team_points;
        [JsonProperty]
        private int blue_team_points;
        [JsonProperty]
        private int time_to_end;
        [JsonProperty]
        private int status;
        #endregion

        #region Model properties
        [JsonIgnore]
        public int RedTeamPoints
        {
            get { return red_team_points; }
            set
            {
                if (red_team_points != value)
                {
                    red_team_points = value;
                    NotifyOfPropertyChange(() => RedTeamPoints);
                }
            }
        }

        [JsonIgnore]
        public int BlueTeamPoints
        {
            get { return blue_team_points; }
            set
            {
                if (blue_team_points != value)
                {
                    blue_team_points = value;
                    NotifyOfPropertyChange(() => BlueTeamPoints);
                }
            }
        }

        [JsonIgnore]
        public int TimeToEnd
        {
            get { return time_to_end; }
            set
            {
                if (time_to_end != value)
                {
                    time_to_end = value;
                    NotifyOfPropertyChange(() => TimeToEnd);
                }
            }
        }

        [JsonIgnore]
        public PreGame.STATUS Status
        {
            get { return (PreGame.STATUS)status; }
            set
            {
                if (status == null || (PreGame.STATUS)status != value)
                {
                    status = (int)value;
                    NotifyOfPropertyChange(() => Status);
                }
            }
        }
        #endregion
    }
}
