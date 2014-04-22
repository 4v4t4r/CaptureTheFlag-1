using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.ViewModels
{
    public class GameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;

        public GameMapViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ILocationService locationService, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            GameMap = new GameMap();
            
            #warning Temporary constants
            GameMap.url = "http://78.133.154.39:8888/api/maps/31/";

            //GameMap.name = "Jasne Blonia";
            //GameMap.description = "description";
            //GameMap.radius = 2500;
            //GameMap.lat = 53.440157f;
            //GameMap.lon = 14.540221f;

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
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void CreateAction()
        {
            GameMap.games = null;
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            communicationService.CreateGameMap(GameMap, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    GameMap = responseGameMap;
                    eventAggregator.Publish(GameMap); //Publish only url string?
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    IsFormAccessible = true;
                }
            );
        }

        public void ReadAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.ReadGameMap(GameMap, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    GameMap = responseData;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void DeleteAction()
        {
            communicationService.DeleteGameMap(GameMap, Token,
            responseGameMap =>
            {
                DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
                IsFormAccessible = true;
            },
            serverErrorMessage =>
            {
                DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                IsFormAccessible = true;
            }
        );
        }

        public void UpdateAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.UpdateGameMap(GameMap, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    GameMap = responseGameMap;
                    eventAggregator.Publish(GameMap);
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    IsFormAccessible = true;
                }
            );
        }

        public void UpdateSelectiveAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            GameMap selectedFields = GameMap;
            communicationService.UpdateGameMapFields(GameMap, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    GameMap = responseGameMap;
                    eventAggregator.Publish(GameMap);
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
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

        private bool shouldCreate;
        public bool ShouldCreate
        {
            get { return shouldCreate; }
            set
            {
                if (shouldCreate != value)
                {
                    shouldCreate = value;
                    NotifyOfPropertyChange(() => ShouldCreate);
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
    }
}
