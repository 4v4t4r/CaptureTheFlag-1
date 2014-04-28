using Caliburn.Micro;

namespace CaptureTheFlag.Models
{
    public class Location : PropertyChangedBase
    {
        private double _lat;
        private double _lon;

        public double lat
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                {
                    _lat = value;
                    NotifyOfPropertyChange(() => lat);
                }
            }
        }

        public double lon
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                {
                    _lon = value;
                    NotifyOfPropertyChange(() => lon);
                }
            }
        }
    }
}
