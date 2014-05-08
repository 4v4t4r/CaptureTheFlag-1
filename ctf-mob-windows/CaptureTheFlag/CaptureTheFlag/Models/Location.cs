namespace CaptureTheFlag.Models
{
    using Caliburn.Micro;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    [DataContract]
    public class Location : PropertyChangedBase
    {
        #region JSON properties
        [JsonProperty(PropertyName = "lat")]
        private double latitude;
        [JsonProperty(PropertyName = "lon")]
        private double longitude;
        #endregion

        #region Model properties
        [IgnoreDataMember]
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    NotifyOfPropertyChange(() => Latitude);
                }
            }
        }

        [IgnoreDataMember]
        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    NotifyOfPropertyChange(() => Longitude);
                }
            }
        }
        #endregion
    }
}
