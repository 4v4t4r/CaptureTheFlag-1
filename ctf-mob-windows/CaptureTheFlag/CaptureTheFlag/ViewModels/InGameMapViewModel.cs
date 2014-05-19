namespace CaptureTheFlag.ViewModels
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using Microsoft.Phone.Maps.Controls;
    using Microsoft.Phone.Maps.Toolkit;
    using RestSharp;
    using System;
    using System.Device.Location;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Ink;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Windows.Devices.Geolocation;
    public class InGameMapViewModel : Screen
    {
        private readonly CommunicationService communicationService;
        private readonly LocationService locationService;
        private readonly GlobalStorageService globalStorageService;

        public InGameMapViewModel(LocationService locationService, CommunicationService communicationService, GlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.communicationService = communicationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;


            
            ZoomLevel = 18.2;
            //MapCenter = new GeoCoordinate(53.432806, 14.548033);
            GameMapCenter = new GeoCoordinate(53.438732, 14.541759, 0.0);
            GeoMapCenter =  new GeoCoordinate(53.438732, 14.541759, 0.0);

            InGame = new InGame();
            Markers = new BindableCollection<Marker>();
            //InGame.Markers = Markers = MarkerHelper.makeMarkers(10);
            //InGame.GameStatus = new GameStatus() { BlueTeamPoints = 100, RedTeamPoints = 100, TimeToEnd = 300, Status = PreGame.STATUS.IN_PROGRESS };

            MapLayer MapLayer = new MapLayer();
            
            Area = new Ellipse();
            Area.Height = 600;
            Area.Width = 600;
            Area.Opacity = 0.5;
            
            SolidColorBrush fillSolidColorBrush = new SolidColorBrush();
            fillSolidColorBrush.Color = Colors.Red;
            Area.Fill = fillSolidColorBrush;
            
            
            Fill = fillSolidColorBrush;
            MapOverlay overlay = new MapOverlay();
            overlay.Content = Area;
            overlay.GeoCoordinate = GameMapCenter;
            overlay.PositionOrigin = new Point(0.5, 0.5);
            MapLayer.Add(overlay);
            MapLayers = new BindableCollection<MapLayer>();
            MapLayers.Add(MapLayer);

            //Area.Visibility = System.Windows.Visibility.Visible;
            //Area.Opacity = 1.0;
            //// Create a SolidColorBrush with a red color to fill the  
            //// Ellipse with.
            
            //SolidColorBrush borderSolidColorBrush = new SolidColorBrush();

            //// Describes the brush's color using RGB values.  
            //// Each value has a range of 0-255.
            
            
            //Area.StrokeThickness = 2;
            //borderSolidColorBrush.Color = Colors.Black;
            //Area.Stroke = borderSolidColorBrush;
            
            RefreshAppBarItemText = "refresh";
            RefreshIcon = new Uri("/Images/refresh.png", UriKind.Relative);
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
        }
        #endregion

        private static bool isWorking;
        public async void UpdateMarkersAction()
        {
            //for (int i = 0; i < 30; ++i)
            //{
            if(isWorking)
            {
                isWorking = false;
            }
            else
            {
                isWorking = true;
            }

            while (isWorking)
            {
                DateTime startTime = DateTime.Now;
                PlayersPosition = await locationService.getCurrentGeoCoordinateAsync();
                TimeSpan diff = DateTime.Now.Subtract(startTime);
                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Got location in: {0}s{1}ms", diff.Seconds, diff.Milliseconds);
                //position.Latitude = 53.438732;
                //position.Longitude = 14.541759;
                PreGame game = new PreGame() { Url = GameModelKey };
                if (Authenticator.IsValid(Authenticator))
                {
                    IRestResponse response = await communicationService.RegisterPlayersPositionAsync(Authenticator.token, game, PlayersPosition);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "READ: {0}", response.Content);

                            InGame = new CommunicationService.JsondotNETDeserializer().Deserialize<InGame>(response);

                        Markers = InGame.Markers;
                        
                    }
                    else
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "{0}", response.StatusDescription);
                        //TODO: new CommunicationService.JsondotNETDeserializer().Deserialize<ItemErrorType>(response);
                    }
                }
                //    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "End of loop: {0}", i);
                //}
            }
        }

        public void MoveAndShowMarkers()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            MarkerHelper.moveMarkers(Markers);
            string formattedLocations = "><";
            foreach(Marker marker in Markers)
            {
                formattedLocations += String.Format("t {0} : lat {1}, lon {2} ><", marker.type, marker.location.Latitude, marker.location.Longitude);
            }
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), formattedLocations);
        }

        public void RefreshAction()
        {
            UpdateMarkersAction();
        }

        private InGame inGame;
        public InGame InGame
        {
            get { return inGame; }
            set
            {
                if (inGame != value)
                {
                    inGame = value;
                    NotifyOfPropertyChange(() => InGame);
                }
            }
        }

        private SolidColorBrush fill;
        public SolidColorBrush Fill
        {
            get { return fill; }
            set { fill = value; NotifyOfPropertyChange(() => Fill); }
        }

        private BindableCollection<MapLayer> mapLayers;
        public BindableCollection<MapLayer> MapLayers
        {
            get { return mapLayers; }
            set { mapLayers = value; NotifyOfPropertyChange(() => MapLayers); }
        }

        private MapLayer mapLayer;
        public MapLayer MapLayer
        {
            get { return mapLayer; }
            set { mapLayer = value; NotifyOfPropertyChange(() => MapLayer); }
        }

        private Ellipse area;
        public Ellipse Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    NotifyOfPropertyChange(() => Area);
                }
            }
        }

        //IMPORTANT: System.InvalidOperationException: Items must be empty before using Items Source
        //To bypass exception, set a property and add or remove elements.
        //ItemSource cannot be set to new instances (i.e. when deserializing a collection in model)
        private BindableCollection<Marker> markers;
        public BindableCollection<Marker> Markers
        {
            get { return markers; }
            set
            {
                if (markers != value)
                {
                    if (markers == null)
                    {
                        markers = value;
                    }
                    else
                    {
                        markers.Clear();
                        markers.AddRange(value);
                    }
                    NotifyOfPropertyChange(() => Markers);
                }
            }
        }


        private string gameModelKey;
        public string GameModelKey
        {
            get { return gameModelKey; }
            set
            {
                if (gameModelKey != value)
                {
                    gameModelKey = value;
                    NotifyOfPropertyChange(() => GameModelKey);
                }
            }
        }

        private GeoCoordinate gameMapCenter;
        public GeoCoordinate GameMapCenter
        {
            get { return gameMapCenter; }
            set
            {
                if (gameMapCenter != value)
                {
                    gameMapCenter = value;
                    NotifyOfPropertyChange(() => GameMapCenter);
                }
            }
        }

        private GeoCoordinate geoMapCenter;
        public GeoCoordinate GeoMapCenter
        {
            get { return geoMapCenter; }
            set
            {
                if (geoMapCenter != value)
                {
                    geoMapCenter = value;
                    NotifyOfPropertyChange(() => GeoMapCenter);
                }
            }
        }

        private GeoCoordinate playersPosition;
        public GeoCoordinate PlayersPosition
        {
            get { return playersPosition; }
            set
            {
                if (playersPosition != value)
                {
                    playersPosition = value;
                    NotifyOfPropertyChange(() => PlayersPosition);
                }
            }
        }

        private Authenticator authenticator;
        public Authenticator Authenticator
        {
            get { return authenticator; }
            set
            {
                if (authenticator != value)
                {
                    authenticator = value;
                    NotifyOfPropertyChange(() => Authenticator);
                }
            }
        }

        private Uri refreshIcon;
        public Uri RefreshIcon
        {
            get { return refreshIcon; }
            set
            {
                if (refreshIcon != value)
                {
                    refreshIcon = value;
                    NotifyOfPropertyChange(() => RefreshIcon);
                }
            }
        }

        private string refreshAppBarItemText;
        public string RefreshAppBarItemText
        {
            get { return refreshAppBarItemText; }
            set
            {
                if (refreshAppBarItemText != value)
                {
                    refreshAppBarItemText = value;
                    NotifyOfPropertyChange(() => RefreshAppBarItemText);
                }
            }
        }

        //TODO: Draw visibility range
        //TODO: Draw action range

        private double zoomLevel = 15;
        public double ZoomLevel
        {
            get { return zoomLevel; }
            set
            {
                if (zoomLevel != value)
                {
                    zoomLevel = value;
                    if (Area != null)
                    {
                        Area.Height = MarkerHelper.MetersToPixels(150, GeoMapCenter.Latitude, ZoomLevel); //TODO: Calculate from Map radius
                        Area.Width = MarkerHelper.MetersToPixels(150, GeoMapCenter.Latitude, ZoomLevel); //TODO: Calculate from Map radius
                    }
                    NotifyOfPropertyChange(() => ZoomLevel);
                }
            }
        }
    }
}