using CaptureTheFlag.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.Services
{
    public class LocationService : ILocationService
    {

        public async Task<Geoposition> getCurrentLocationAsync()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            Geoposition geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10)
                );
            return geoposition;
        }

        public Task<GeoCoordinate> getCurrentGeoCoordinateAsync()
        {
            return Task<GeoCoordinate>.Run(() =>
                {
                    Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    Watcher.Start();
                    GeoCoordinate location = Watcher.Position.Location;
                    Watcher.Stop();
                    return location;
                });
        }

        private GeoCoordinateWatcher watcher;
        public GeoCoordinateWatcher Watcher
        {
            get { return watcher; }
            set
            {
                if (watcher != value)
                {
                    watcher = value;
                }
            }
        }
        public void RegisterPositionAction()
        {
            Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            Watcher.Start();
            GeoCoordinate loc = Watcher.Position.Location;
            Watcher.Stop();
        }

        public RestRequestAsyncHandle RegisterPositionCommunicationAction(ICommunicationService communicationService, Authenticator authenticator, Game game)
        {
            RestRequestAsyncHandle requestHandle = null;
            //TODO: Response object model
            Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            Watcher.Start();
            if (Authenticator.IsValid(authenticator))
            {
                requestHandle = communicationService.RegisterPosition(game, Watcher.Position.Location, authenticator.token,
                    rData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                    }
                );
            }
            Watcher.Stop();
            return requestHandle;
        }

        //TODO: move it in its own class?
        public GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {
            return new GeoCoordinate
                (
                geocoordinate.Latitude,
                geocoordinate.Longitude,
                geocoordinate.Altitude ?? Double.NaN,
                geocoordinate.Accuracy,
                geocoordinate.AltitudeAccuracy ?? Double.NaN,
                geocoordinate.Speed ?? Double.NaN,
                geocoordinate.Heading ?? Double.NaN
                );
        }
    }
}
