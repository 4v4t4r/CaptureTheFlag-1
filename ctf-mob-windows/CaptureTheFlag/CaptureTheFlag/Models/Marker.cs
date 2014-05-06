using Caliburn.Micro;
using System;
using System.Device.Location;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace CaptureTheFlag.Models
{
    public class Marker : PropertyChangedBase
    {
        public int type { get; set; }
        public Location location { get; set; }
        public string url { get; set; }

        public GeoCoordinate Position { 
            get
            {
                return new GeoCoordinate(location.lat, location.lon);
            }
            set
            {
                location.lat = value.Latitude;
                location.lon = value.Longitude;
                NotifyOfPropertyChange(() => Position);
            }
        }

        private System.Windows.Shapes.Ellipse area = new System.Windows.Shapes.Ellipse();
        public System.Windows.Shapes.Ellipse Area
        {
            get { 
                area.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                area.Width = 40;
                area.Height = 40;
                return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    NotifyOfPropertyChange(() => Area);
                }
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
    }
}
