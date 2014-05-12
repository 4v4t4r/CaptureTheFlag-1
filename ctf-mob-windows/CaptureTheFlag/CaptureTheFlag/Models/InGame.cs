namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using Newtonsoft.Json;
    public class InGame : PropertyChangedBase
    {
        #region JSON properties
        [JsonProperty("game")]
        private GameStatus gameStatus;
        [JsonProperty]
        private BindableCollection<Marker> markers;
        #endregion

        #region Model properties
        [JsonIgnore]
        public GameStatus GameStatus
        {
            get { return gameStatus; }
            set
            {
                if (gameStatus != value)
                {
                    gameStatus = value;
                    NotifyOfPropertyChange(() => GameStatus);
                }
            }
        }

        [JsonIgnore]
        public BindableCollection<Marker> Markers
        {
            get { return markers; }
            set
            {
                if (markers != value)
                {
                    markers = value;
                    NotifyOfPropertyChange(() => Markers);
                }
            }
        }
        #endregion
    }
}
