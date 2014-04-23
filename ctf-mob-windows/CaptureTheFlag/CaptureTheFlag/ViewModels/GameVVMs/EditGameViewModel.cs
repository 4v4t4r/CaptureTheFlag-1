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

namespace CaptureTheFlag.ViewModels.GameVVMs
{

    public class EditGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;

        public EditGameViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            Game = new Game();
            //TODO: update to model probably
            StartDate = DateTime.Now.AddDays(1);

            DisplayName = "Edit game";

            UpdateAppBarItemText = "update";
            UpdateIcon = new Uri("/Images/upload.png", UriKind.Relative);

            UpdateSelectiveAppBarItemText = "selective update";
            UpdateSelectiveIcon = new Uri("/Images/share.png", UriKind.Relative);

            DeleteAppBarMenuItemText = "delete";

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
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Game.url = GameModelKey;

            if (!String.IsNullOrEmpty(Game.url))
            {
                if (globalStorageService.Current.Games.ContainsKey(Game.url))
                {
                    Game = globalStorageService.Current.Games[Game.url];
                }
                else
                {
                    ReadAction();
                }
            }
        }
        #endregion

        #region Actions
        public void ReadAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.ReadGame(Game, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        Game = responseData;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void DeleteAction()
        {
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.DeleteGame(Game, token,
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
        }

        public void UpdateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.UpdateGame(Game, token,
                    responseGameMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Game = responseGameMap;
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
        }

        public void UpdateSelectiveAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                Game selectedFields = Game;
                communicationService.UpdateGameFields(Game, token,
                    responseGameMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Game = responseGameMap;
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

        private string deleteAppBarMenuItemText;
        public string DeleteAppBarMenuItemText
        {
            get { return deleteAppBarMenuItemText; }
            set
            {
                if (deleteAppBarMenuItemText != value)
                {
                    deleteAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => DeleteAppBarMenuItemText);
                }
            }
        }

        private Uri updateIcon;
        public Uri UpdateIcon
        {
            get { return updateIcon; }
            set
            {
                if (updateIcon != value)
                {
                    updateIcon = value;
                    NotifyOfPropertyChange(() => UpdateIcon);
                }
            }
        }

        private Uri updateSelectiveIcon;
        public Uri UpdateSelectiveIcon
        {
            get { return updateSelectiveIcon; }
            set
            {
                if (updateSelectiveIcon != value)
                {
                    updateSelectiveIcon = value;
                    NotifyOfPropertyChange(() => UpdateSelectiveIcon);
                }
            }
        }

        private string updateAppBarItemText;
        public string UpdateAppBarItemText
        {
            get { return updateAppBarItemText; }
            set
            {
                if (updateAppBarItemText != value)
                {
                    updateAppBarItemText = value;
                    NotifyOfPropertyChange(() => UpdateAppBarItemText);
                }
            }
        }

        private string updateSelectiveAppBarItemText;
        public string UpdateSelectiveAppBarItemText
        {
            get { return updateSelectiveAppBarItemText; }
            set
            {
                if (updateSelectiveAppBarItemText != value)
                {
                    updateSelectiveAppBarItemText = value;
                    NotifyOfPropertyChange(() => UpdateSelectiveAppBarItemText);
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
    }
}
