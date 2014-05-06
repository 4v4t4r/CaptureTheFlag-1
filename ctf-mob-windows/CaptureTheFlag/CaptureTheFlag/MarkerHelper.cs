namespace CaptureTheFlag
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using System;

    public class MarkerHelper
    {
        static readonly double MIN_LATITUDE = 53.432217;
        static readonly double MAX_LATITUDE = 53.433300;
        static readonly double MIN_LONGITUDE = 14.547190;
        static readonly double MAX_LONGITUDE = 14.548988;
        static readonly Random random = new Random();

        static public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        static public BindableCollection<Marker> makeMarkers(uint count)
        {
            
            BindableCollection<Marker> markers = new BindableCollection<Marker>();
            for (int it = 0; it < count; ++it)
            {
                Marker marker = new Marker();
                marker.type = random.Next(0, 8); //NOTE: upper bound NOT inclusive
                marker.url = String.Format("http://78.133.154.39:8888/{0}/{1}", marker.type, it);
                marker.location = new Location();
                marker.location.lat = GetRandomNumber(MIN_LATITUDE, MAX_LATITUDE);
                marker.location.lon = GetRandomNumber(MIN_LONGITUDE, MAX_LONGITUDE);
                markers.Add(marker);
            }
            return markers;
        }

        static public void moveMarkers(BindableCollection<Marker> markers)
        {
           foreach(Marker marker in markers)
           {
               marker.location.lat = GetRandomNumber(MIN_LATITUDE, MAX_LATITUDE);
               marker.location.lon = GetRandomNumber(MIN_LONGITUDE, MAX_LONGITUDE);
               marker.Position = new System.Device.Location.GeoCoordinate(marker.location.lat, marker.location.lon);
           }
        }
    }
}
