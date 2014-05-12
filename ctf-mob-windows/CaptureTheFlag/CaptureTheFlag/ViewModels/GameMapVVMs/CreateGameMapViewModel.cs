using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameVVMs;
using Microsoft.Phone.Maps.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    public class CreateGameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ILocationService locationService;
        private readonly IGlobalStorageService globalStorageService;
        private readonly ICommunicationService communicationService;

        public CreateGameMapViewModel(INavigationService navigationService, ILocationService locationService, IGlobalStorageService globalStorageService, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;
            this.communicationService = communicationService;

            Game = new Game();
            ZoomLevel = 18.2;
            MapCenter = new GeoCoordinate(53.432806, 14.548033, 0.0);
            MapCenter.HorizontalAccuracy = 50.0;

            GameMap = new GameMap();
            AreaCenter = new GeoCoordinate(MapCenter.Latitude, MapCenter.Longitude, 0.0);
            Area = new Ellipse();
            GameMap.radius = 100.0;
            Area.Height = Area.Width = MarkerHelper.MetersToPixels(GameMap.radius, MapCenter.Latitude, ZoomLevel) * 2.0; //TODO: Calculate from Map radius
            Area.Opacity = 0.5;
            Area.Fill = new SolidColorBrush() { Color = Colors.Red };

            Markers = new BindableCollection<Marker>();
            Items = new BindableCollection<Item>();

            DisplayName = "Game map";

            ApplyMapChangesAppBarItemText = "apply";
            ApplyMapChangesIcon  = new Uri("/Images/check.png", UriKind.Relative);

            SetGameAreaAppBarItemText = "area";
            SetGameAreaIcon = new Uri("/Images/share.png", UriKind.Relative);

            AddRedBaseAppBarItemText = "red base";
            AddRedBaseIcon = new Uri("/Images/like.png", UriKind.Relative);

            AddBlueBaseAppBarItemText = "blue base";
            AddBlueBaseIcon = new Uri("/Images/upload.png", UriKind.Relative);

            CanChangeGameRadius = false;
            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            if (!String.IsNullOrEmpty(GameModelKey) && globalStorageService.Current.Games.ContainsKey(GameModelKey))
            {
                Game = globalStorageService.Current.Games[GameModelKey];
            }
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void ApplyMapChangesAction()
        {
            //TODO: Remove the key if it is not used or use an object specific for game creation
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            //Game.Items = Markers; //TODO: Change markers to items binding
            if (GameModelKey == null)
            {
                GameModelKey = "TemporaryGameModelKey";
            }
            Game.Radius = GameMap.radius;
            globalStorageService.Current.Games[GameModelKey] = Game;
            globalStorageService.Current.Items = Items;
            
            navigationService.UriFor<CreateGameViewModel>()
                .WithParam(param => param.GameModelKey, GameModelKey)
                .Navigate();
            navigationService.RemoveBackEntry();
        }


        public void SetGameAreaAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            CanChangeGameRadius = !CanChangeGameRadius;
            if (CanChangeGameRadius)
            {
                MapCenter = AreaCenter;
            }
        }

        public void AddRedBaseAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());

            Marker redBase = new Marker();
            redBase.type = 5;
            redBase.location = new Location() { Latitude = MapCenter.Latitude, Longitude = MapCenter.Longitude };
            //redBase.url = String.Format("http://78.133.154.39:8888/{0}/{1}", redBase.type, redBase.type); //TODO: Remove, temporary const

            Item redBaseItem = new Item();
            redBaseItem.Type = Item.ITEM_TYPE.RED_BASE;
            redBaseItem.Location = redBase.location;

            var idx = Markers.ToList().FindIndex(marker => marker.type == 5);
            if(idx != -1)
            {
                Markers[idx] = redBase;
                Items[idx] = redBaseItem;
            }
            else
            {
                Markers.Add(redBase);
                Items.Add(redBaseItem);
            }
        }

        public void AddBlueBaseAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());

            Marker blueBase = new Marker();
            blueBase.type = 4;
            blueBase.location = new Location() { Latitude = MapCenter.Latitude, Longitude = MapCenter.Longitude };
            //blueBase.url = String.Format("http://78.133.154.39:8888/{0}/{1}", blueBase.type, blueBase.type); //TODO: Remove, temporary const

            Item blueBaseItem = new Item();
            blueBaseItem.Type = Item.ITEM_TYPE.RED_BASE;
            blueBaseItem.Location = blueBase.location;

            var idx = Markers.ToList().FindIndex(marker => marker.type == 4);
            if (idx != -1)
            {
                Markers[idx] = blueBase;
                Items[idx] = blueBaseItem;
            }
            else
            {
                Markers.Add(blueBase);
                Items.Add(blueBaseItem);
            }
        }

        public void ChangeGameFieldAtion()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (Area != null && GameMap != null && MapCenter != null)
            {
                if (CanChangeGameRadius)
                {
                    //TODO: check validity of calculations
                    double pixelsRadius = Area.ActualWidth / 2.0;
                    GameMap.radius = MarkerHelper.PixelsToMeters(pixelsRadius, MapCenter.Latitude, ZoomLevel);
                    AreaCenter = MapCenter;
                    GameMap.lat = AreaCenter.Latitude;
                    GameMap.lon = AreaCenter.Longitude;
                }
                else
                {
                    double pixelsRadius = MarkerHelper.MetersToPixels(GameMap.radius, MapCenter.Latitude, ZoomLevel);
                    Area.Height = Area.Width = pixelsRadius * 2.0; //TODO: Calculate from Map radius
                }
            }
        }
        #endregion

        #region Properties
        #region Model properties
        private Game game;
        public Game Game
        {
            get { return game; }
            set
            {
                if (game != value)
                {
                    game = value;
                    NotifyOfPropertyChange(() => Game);
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

        private GameMap gameMap;
        public GameMap GameMap
        {
            get { return gameMap; }
            set
            {
                if (gameMap != value)
                {
                    gameMap = value;
                    NotifyOfPropertyChange(() => GameMap);
                }
            }
        }

        private string gameMapModelKey;
        public string GameMapModelKey
        {
            get { return gameMapModelKey; }
            set
            {
                if (gameMapModelKey != value)
                {
                    gameMapModelKey = value;
                    NotifyOfPropertyChange(() => GameMapModelKey);
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

        private BindableCollection<Marker> markers;
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

        private BindableCollection<Item> items;
        public BindableCollection<Item> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    NotifyOfPropertyChange(() => Items);
                }
            }
        }
        #endregion

        #region UI properties
        private string applyMapChangesAppBarItemText;
        public string ApplyMapChangesAppBarItemText
        {
            get { return applyMapChangesAppBarItemText; }
            set
            {
                if (applyMapChangesAppBarItemText != value)
                {
                    applyMapChangesAppBarItemText = value;
                    NotifyOfPropertyChange(() => ApplyMapChangesAppBarItemText);
                }
            }
        }

        private Uri applyMapChangesIcon;
        public Uri ApplyMapChangesIcon
        {
            get { return applyMapChangesIcon; }
            set
            {
                if (applyMapChangesIcon != value)
                {
                    applyMapChangesIcon = value;
                    NotifyOfPropertyChange(() => ApplyMapChangesIcon);
                }
            }
        }

        private string setGameAreaAppBarItemText;
        public string SetGameAreaAppBarItemText
        {
            get { return setGameAreaAppBarItemText; }
            set
            {
                if (setGameAreaAppBarItemText != value)
                {
                    setGameAreaAppBarItemText = value;
                    NotifyOfPropertyChange(() => SetGameAreaAppBarItemText);
                }
            }
        }

        private Uri setGameAreaIcon;
        public Uri SetGameAreaIcon
        {
            get { return setGameAreaIcon; }
            set
            {
                if (setGameAreaIcon != value)
                {
                    setGameAreaIcon = value;
                    NotifyOfPropertyChange(() => SetGameAreaIcon);
                }
            }
        }

        private string addRedBaseAppBarItemText;
        public string AddRedBaseAppBarItemText
        {
            get { return addRedBaseAppBarItemText; }
            set
            {
                if (addRedBaseAppBarItemText != value)
                {
                    addRedBaseAppBarItemText = value;
                    NotifyOfPropertyChange(() => AddRedBaseAppBarItemText);
                }
            }
        }

        private Uri addRedBaseIcon;
        public Uri AddRedBaseIcon
        {
            get { return addRedBaseIcon; }
            set
            {
                if (addRedBaseIcon != value)
                {
                    addRedBaseIcon = value;
                    NotifyOfPropertyChange(() => AddRedBaseIcon);
                }
            }
        }

        private string addBlueBaseAppBarItemText;
        public string AddBlueBaseAppBarItemText
        {
            get { return addBlueBaseAppBarItemText; }
            set
            {
                if (addBlueBaseAppBarItemText != value)
                {
                    addBlueBaseAppBarItemText = value;
                    NotifyOfPropertyChange(() => AddBlueBaseAppBarItemText);
                }
            }
        }

        private Uri addBlueBaseIcon;
        public Uri AddBlueBaseIcon
        {
            get { return addBlueBaseIcon; }
            set
            {
                if (addBlueBaseIcon != value)
                {
                    addBlueBaseIcon = value;
                    NotifyOfPropertyChange(() => AddBlueBaseIcon);
                }
            }
        }

        private bool isFormAccessible;
        public bool IsFormAccessible
        {
            get { return isFormAccessible; }
            set
            {
                if (isFormAccessible != value)
                {
                    isFormAccessible = value;
                    NotifyOfPropertyChange(() => IsFormAccessible);
                }
            }
        }
        #endregion
        #endregion



        //-------------------------------------------------------------------


        private GeoCoordinate mapCenter = new GeoCoordinate(53.432806, 14.548033);
        public GeoCoordinate MapCenter
        {
            get { return mapCenter; }
            set
            {
                if (mapCenter != value)
                {
                    mapCenter = value;
                    ChangeGameFieldAtion();
                    NotifyOfPropertyChange(() => MapCenter);
                }
            }
        }

        private GeoCoordinate areaCenter;
        public GeoCoordinate AreaCenter
        {
            get { return areaCenter; }
            set
            {
                if (areaCenter != value)
                {
                    areaCenter = value;
                    if(Game == null)
                    {
                        Game = new Game();
                    }
                    if(Game.Location == null)
                    {
                        Game.Location = new Location();
                    }
                    Game.Location.Latitude = areaCenter.Latitude;
                    Game.Location.Longitude = areaCenter.Longitude;
                    NotifyOfPropertyChange(() => AreaCenter);
                }
            }
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
                    if (Game == null)
                    {
                        Game = new Game();
                    }
                    double pixelsRadius = area.ActualWidth / 2.0;
                    Game.Radius = MarkerHelper.PixelsToMeters(pixelsRadius, MapCenter.Latitude, ZoomLevel);
                    NotifyOfPropertyChange(() => Area);
                }
            }
        }

        private bool canChangeGameRadius;
        public bool CanChangeGameRadius
        {
            get { return canChangeGameRadius; }
            set
            {
                if (canChangeGameRadius != value)
                {
                    canChangeGameRadius = value;
                    NotifyOfPropertyChange(() => CanChangeGameRadius);
                }
            }
        }

        private double zoomLevel = 15;
        public double ZoomLevel
        {
            get { return zoomLevel; }
            set
            {
                if (zoomLevel != value)
                {
                    zoomLevel = value;
                    ChangeGameFieldAtion();
                    NotifyOfPropertyChange(() => ZoomLevel);
                }
            }
        }
    }
}
