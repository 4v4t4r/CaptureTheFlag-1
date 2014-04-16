using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class CreateGameViewModel : Screen, IHandle<GameMap>
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;

        public CreateGameViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            IsFormAccessible = true;

            Game = new Game();
            //TODO: update to model probably
            StartDate = DateTime.Now.AddDays(1);

            DisplayName = "Create game";
            AddUserButton = "Subscribe me";
            RemoveUserButton = "Unsubscribe me";
            CreateButton = "Create new";
            ReadButton = "Read";
            UpdateButton = "Update";
            UpdateSelectiveButton = "Fast Update";
            DeleteButton = "Delete";
            NameTextBlock = "Name:";
            DescriptionTextBlock = "Description:";
            StartTimeTextBlock = "Start time:";
            MaxPlayersTextBlock = "Max players:";
            GameTypeTextBlock = "Game type:";
            VisibilityRangeTextBlock = "Visibility range:";
            ActionRangeTextBlock = "Action range:";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
            Game.url = GameModelKey;
            //Game = IoC.Get<GlobalStorageService>().Current.Games[GameModelKey];
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            if(close)
            {
                eventAggregator.Unsubscribe(this);
            }
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void CreateAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.CreateGame(Game, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    Game = responseGameMap;
                    eventAggregator.Publish(Game);
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

        public void AddUserAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.AddPlayerToGame(Game, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "added", MessageBoxButton.OK);
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void RemoveUserAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.RemovePlayerFromGame(Game, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "added", MessageBoxButton.OK);
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void ReadAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.ReadGame(Game, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    Game = responseData;
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
            communicationService.DeleteGame(Game, Token,
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
            communicationService.UpdateGame(Game, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    Game = responseGameMap;
                    eventAggregator.Publish(Game);
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
            Game selectedFields = Game;
            selectedFields.name = null;
            communicationService.UpdateGameFields(Game, Token,
                responseGameMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    Game = responseGameMap;
                    eventAggregator.Publish(Game);
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



        #region Message Handling
        public void Handle(GameMap gameMap)
        {
            Debug.WriteLine("Mplayers {0}", gameMap.url);
            Game.map = gameMap.url;
        }
        #endregion

        #region Properties

        #region Model Properties
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

        //TODO: date time in model maybe?
        //TODO: make aconverter
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    if (Game != null)
                    {
                        Game.start_time = startDate.ToString("s");
                    }
                    NotifyOfPropertyChange(() => StartDate);
                }
            }
        }
        #endregion

        #region UI Properties
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

        private string startTimeTextBlock;
        public string StartTimeTextBlock
        {
            get { return startTimeTextBlock; }
            set
            {
                if (startTimeTextBlock != value)
                {
                    startTimeTextBlock = value;
                    NotifyOfPropertyChange(() => StartTimeTextBlock);
                }
            }
        }

        private string maxPlayersTextBlock;
        public string MaxPlayersTextBlock
        {
            get { return maxPlayersTextBlock; }
            set
            {
                if (maxPlayersTextBlock != value)
                {
                    maxPlayersTextBlock = value;
                    NotifyOfPropertyChange(() => MaxPlayersTextBlock);
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

        private string visibilityRangeTextBlock;
        public string VisibilityRangeTextBlock
        {
            get { return visibilityRangeTextBlock; }
            set
            {
                if (visibilityRangeTextBlock != value)
                {
                    visibilityRangeTextBlock = value;
                    NotifyOfPropertyChange(() => VisibilityRangeTextBlock);
                }
            }
        }

        private string actionRangeTextBlock;
        public string ActionRangeTextBlock
        {
            get { return actionRangeTextBlock; }
            set
            {
                if (actionRangeTextBlock != value)
                {
                    actionRangeTextBlock = value;
                    NotifyOfPropertyChange(() => ActionRangeTextBlock);
                }
            }
        }

        private string addUserButton;
        public string AddUserButton
        {
            get { return addUserButton; }
            set
            {
                if (addUserButton != value)
                {
                    addUserButton = value;
                    NotifyOfPropertyChange(() => AddUserButton);
                }
            }
        }

        private string removeUserButton;
        public string RemoveUserButton
        {
            get { return removeUserButton; }
            set
            {
                if (removeUserButton != value)
                {
                    removeUserButton = value;
                    NotifyOfPropertyChange(() => RemoveUserButton);
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
        #endregion
    }
}
