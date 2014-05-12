using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Device.Location;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace CaptureTheFlag.Models
{
    [DataContract]
    public class Marker : PropertyChangedBase
    {
        #region JSON properties
        [JsonProperty]
        public int type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Location location { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
        [JsonProperty]
        private double distance;
        #endregion

        #region Model properties
        public GeoCoordinate Position { 
            get
            {
                return new GeoCoordinate(location.Latitude, location.Longitude);
            }
            set
            {
                location.Latitude = value.Latitude;
                location.Longitude = value.Longitude;
                NotifyOfPropertyChange(() => Position);
            }
        }

        private Image image = new Image();
        public Image Image
        {
            get
            {
                string imgPath = "/Images/refresh.png";
                switch (type)
                {
                    case 0:
                        imgPath = "/Images/InGame/blue_flag.png";
                        break;
                    case 1:
                        imgPath = "/Images/InGame/red_flag.png";
                        break;
                    case 2:
                        imgPath = "/Images/InGame/blue_player.png";
                        break;
                    case 3:
                        imgPath = "/Images/InGame/red_player.png";
                        break;
                    case 4:
                        imgPath = "/Images/InGame/blue_base.png";
                        break;
                    case 5:
                        imgPath = "/Images/InGame/red_base.png";
                        break;
                    case 6:
                        imgPath = "/Images/InGame/red_player_with_flag.png";
                        break;
                    case 7:
                        imgPath = "/Images/InGame/blue_player_with_flag.png";
                        break;
                }
                BitmapImage myImage = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
                var image = new Image();
                image.Width = 64;
                image.Height = 64;
                image.Opacity = 100;
                image.Source = myImage;
                return image;
            }
            set
            {
                if (image != value)
                {
                    image = value;
                    NotifyOfPropertyChange(() => Image);
                }
            }
        }

        [JsonIgnore]
        public double Distance
        {
            get { return distance; }
            set
            {
                if (distance != value)
                {
                    distance = value;
                    NotifyOfPropertyChange(() => Distance);
                }
            }
        }
        #endregion
    }
}
