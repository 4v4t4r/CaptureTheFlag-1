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

        //Reference: http://msdn.microsoft.com/en-us/library/aa940990.aspx
        private static double ResolutionMSDN(double latitude, double zoomLevel)
        {
            return 156543.04 * Math.Cos(latitude) / Math.Pow(2.0, zoomLevel);
        }

        //Reference: http://developer.nokia.com/resources/library/Lumia/maps-and-navigation/guide-to-the-wp8-maps-api.html
        private static double ResolutionNokia(double latitude, double zoomLevel)
        {
            return (Math.Cos(latitude * Math.PI / 180.0) * 2.0 * Math.PI * 6378137.0) / (256.0 * Math.Pow(2.0, zoomLevel));
        }

        private static double MetersPixelsResolution(double latitude, double zoomLevel)
        {
            return ResolutionNokia(latitude, zoomLevel);
        }

        //TODO: include accuracy in calculations!
        public static double MetersToPixels(double meters, double latitude, double zoomLevel)
        {
            return Math.Abs(meters / MetersPixelsResolution(latitude, zoomLevel));
        }

        public static double PixelsToMeters(double pixels, double latitude, double zoomLevel)
        {
            return Math.Abs(pixels * MetersPixelsResolution(latitude, zoomLevel));
        }

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
                marker.location.Latitude = GetRandomNumber(MIN_LATITUDE, MAX_LATITUDE);
                marker.location.Longitude = GetRandomNumber(MIN_LONGITUDE, MAX_LONGITUDE);
                markers.Add(marker);
            }
            return markers;
        }

        static public void moveMarkers(BindableCollection<Marker> markers)
        {
           foreach(Marker marker in markers)
           {
               marker.location.Latitude = GetRandomNumber(MIN_LATITUDE, MAX_LATITUDE);
               marker.location.Longitude = GetRandomNumber(MIN_LONGITUDE, MAX_LONGITUDE);
               marker.Position = new System.Device.Location.GeoCoordinate(marker.location.Latitude, marker.location.Longitude);
           }
        }
    }
}
