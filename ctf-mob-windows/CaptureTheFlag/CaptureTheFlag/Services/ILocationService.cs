using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.Services
{
    public interface ILocationService
    {
        Task<Geoposition> getCurrentLocationAsync();
        GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate);

        Task<GeoCoordinate> getCurrentGeoCoordinateAsync();
    }
}
