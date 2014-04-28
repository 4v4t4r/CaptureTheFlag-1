using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    public class CreateGameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;
        private readonly ILocationService locationService;

        public CreateGameMapViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ILocationService locationService, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;
            this.locationService = locationService;

            GameMap = new GameMap();

            DisplayName = "Game map";
            NameTextBlock = "Name:";
            DescriptionTextBlock = "Description:";
            RadiusTextBlock = "Radius:";
            LatTextBlock = "Latitude:";
            LonTextBlock = "Longitude";
            CreateButton = "Create";

            CreateButton = "Create new";
            ReadButton = "Read";
            UpdateButton = "Update";
            UpdateSelectiveButton = "Fast Update";
            DeleteButton = "Delete";
            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void CreateAction()
        {
            GameMap.games = null;
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            communicationService.CreateGameMap(GameMap, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    GameMap = responseGameMap;
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    IsFormAccessible = true;
                }
            );
        }

        #endregion

        #region Properties
        #region Model properties
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

        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                if (token != value)
                {
                    token = value;
                    NotifyOfPropertyChange(() => Token);
                }
            }
        }
        #endregion

        #region UI properties
        private Map geoMap;
        public Map GeoMap
        {
            get { return geoMap; }
            set
            {
                if (geoMap != value)
                {
                    geoMap = value;
                    NotifyOfPropertyChange(() => GeoMap);
                }
            }
        }

        private string nameTextBlock;
        public string NameTextBlock
        {
            get { return nameTextBlock; }
            set
            {
                if (nameTextBlock != value)
                {
                    nameTextBlock = value;
                    NotifyOfPropertyChange(() => NameTextBlock);
                }
            }
        }

        private string descriptionTextBlock;
        public string DescriptionTextBlock
        {
            get { return descriptionTextBlock; }
            set
            {
                if (descriptionTextBlock != value)
                {
                    descriptionTextBlock = value;
                    NotifyOfPropertyChange(() => DescriptionTextBlock);
                }
            }
        }

        private string radiusTextBlock;
        public string RadiusTextBlock
        {
            get { return radiusTextBlock; }
            set
            {
                if (radiusTextBlock != value)
                {
                    radiusTextBlock = value;
                    NotifyOfPropertyChange(() => RadiusTextBlock);
                }
            }
        }

        private string latTextBlock;
        public string LatTextBlock
        {
            get { return latTextBlock; }
            set
            {
                if (latTextBlock != value)
                {
                    latTextBlock = value;
                    NotifyOfPropertyChange(() => LatTextBlock);
                }
            }
        }

        private string lonTextBlock;
        public string LonTextBlock
        {
            get { return lonTextBlock; }
            set
            {
                if (lonTextBlock != value)
                {
                    lonTextBlock = value;
                    NotifyOfPropertyChange(() => LonTextBlock);
                }
            }
        }

        private string createButton;
        public string CreateButton
        {
            get { return createButton; }
            set
            {
                if (createButton != value)
                {
                    createButton = value;
                    NotifyOfPropertyChange(() => CreateButton);
                }
            }
        }
        private string readButton;
        public string ReadButton
        {
            get { return readButton; }
            set
            {
                if (readButton != value)
                {
                    readButton = value;
                    NotifyOfPropertyChange(() => ReadButton);
                }
            }
        }


        private string updateButton;
        public string UpdateButton
        {
            get { return updateButton; }
            set
            {
                if (updateButton != value)
                {
                    updateButton = value;
                    NotifyOfPropertyChange(() => UpdateButton);
                }
            }
        }

        private string updateSelectiveButton;
        public string UpdateSelectiveButton
        {
            get { return updateSelectiveButton; }
            set
            {
                if (updateSelectiveButton != value)
                {
                    updateSelectiveButton = value;
                    NotifyOfPropertyChange(() => UpdateSelectiveButton);
                }
            }
        }

        private string deleteButton;
        public string DeleteButton
        {
            get { return deleteButton; }
            set
            {
                if (deleteButton != value)
                {
                    deleteButton = value;
                    NotifyOfPropertyChange(() => DeleteButton);
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
                    eventAggregator.Publish(isFormAccessible);
                }
            }
        }
        #endregion
        #endregion



        //-------------------------------------------------------------------


        private GeoCoordinate mapCenter = new GeoCoordinate(40.712923, -74.013292);
        public GeoCoordinate MapCenter
        {
            get { return mapCenter; }
            set
            {
                if (mapCenter != value)
                {
                    mapCenter = value;
                    NotifyOfPropertyChange(() => MapCenter);
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
                    NotifyOfPropertyChange(() => ZoomLevel);
                }
            }
        }
    }
}
