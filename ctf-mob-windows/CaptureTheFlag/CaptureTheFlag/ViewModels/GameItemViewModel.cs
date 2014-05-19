using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class GameItemViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly CommunicationService communicationService;

        public GameItemViewModel(INavigationService navigationService, IEventAggregator eventAggregator, LocationService locationService, CommunicationService communicationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            GameItem = new GameItem();
            
            #warning Temporary constants
            gameItemModelKey = "http://78.133.154.39:8888/api/items/1/";
            GameItem.game = "http://78.133.154.39:8888/api/games/4/";
            GameItem.url = "http://78.133.154.39:8888/api/items/1/";

            //GameMap.name = "Jasne Blonia";
            //GameMap.description = "description";
            //GameMap.radius = 2500;
            //GameMap.lat = 53.440157f;
            //GameMap.lon = 14.540221f;

            DisplayName = "Game item";
            NameTextBlock = "Name:";
            GameTypeTextBlock = "Item type:";
            DescriptionTextBlock = "Description:";
            ValueTextBlock = "Value:";
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
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            communicationService.CreateGameItem(GameItem, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    GameItem = responseGameMap;
                    eventAggregator.Publish(GameItem); //Publish only url string?
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

        public void ReadAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.ReadGameItem(GameItem, Token,
                responseData =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    GameItem = responseData;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void DeleteAction()
        {
            communicationService.DeleteGameItem(GameItem, Token,
            responseGameMap =>
            {
                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
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

        public void UpdateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.UpdateGameItem(GameItem, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    GameItem = responseGameMap;
                    eventAggregator.Publish(GameItem);
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

        public void UpdateSelectiveAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            GameItem selectedFields = GameItem;
            communicationService.UpdateGameItemFields(GameItem, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    GameItem = responseGameMap;
                    eventAggregator.Publish(GameItem);
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
        private GameItem gameItem;
        public GameItem GameItem
        {
            get { return gameItem; }
            set
            {
                if (gameItem != value)
                {
                    gameItem = value;
                    NotifyOfPropertyChange(() => GameItem);
                }
            }
        }

        private string gameItemModelKey;
        public string GameItemModelKey
        {
            get { return gameItemModelKey; }
            set
            {
                if (gameItemModelKey != value)
                {
                    gameItemModelKey = value;
                    NotifyOfPropertyChange(() => GameItemModelKey);
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

        private string gameTypeTextBlock;
        public string GameTypeTextBlock
        {
            get { return gameTypeTextBlock; }
            set
            {
                if (gameTypeTextBlock != value)
                {
                    gameTypeTextBlock = value;
                    NotifyOfPropertyChange(() => GameTypeTextBlock);
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

        private string valueTextBlock;
        public string ValueTextBlock
        {
            get { return valueTextBlock; }
            set
            {
                if (valueTextBlock != value)
                {
                    valueTextBlock = value;
                    NotifyOfPropertyChange(() => ValueTextBlock);
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
