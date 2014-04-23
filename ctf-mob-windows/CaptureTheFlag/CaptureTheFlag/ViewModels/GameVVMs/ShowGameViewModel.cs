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

    public class ShowGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;

        public ShowGameViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            Game = new Game();
            //TODO: update to model probably
            StartDate = DateTime.Now.AddDays(1);

            DisplayName = "Game Details";

            AddUserAppBarItemText = "subscribe me";
            AddUserIcon = new Uri("/Images/add.png", UriKind.Relative);

            RemoveUserAppBarItemText = "unsubscribe me";
            RemoveUserIcon = new Uri("/Images/minus.png", UriKind.Relative);

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
            //Game = IoC.Get<GlobalStorageService>().Current.Games[GameModelKey];

            if( ! String.IsNullOrEmpty( Game.url ) )
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
 
        public void AddUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.AddPlayerToGame(Game, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "added", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void RemoveUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.RemovePlayerFromGame(Game, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "added", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

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

        private string addUserAppBarItemText;
        public string AddUserAppBarItemText
        {
            get { return addUserAppBarItemText; }
            set
            {
                if (addUserAppBarItemText != value)
                {
                    addUserAppBarItemText = value;
                    NotifyOfPropertyChange(() => AddUserAppBarItemText);
                }
            }
        }

        private Uri addUserIcon;
        public Uri AddUserIcon
        {
            get { return addUserIcon; }
            set
            {
                if (addUserIcon != value)
                {
                    addUserIcon = value;
                    NotifyOfPropertyChange(() => AddUserIcon);
                }
            }
        }

        private Uri removeUserIcon;
        public Uri RemoveUserIcon
        {
            get { return removeUserIcon; }
            set
            {
                if (removeUserIcon != value)
                {
                    removeUserIcon = value;
                    NotifyOfPropertyChange(() => RemoveUserIcon);
                }
            }
        }

        private string removeUserAppBarItemText;
        public string RemoveUserAppBarItemText
        {
            get { return removeUserAppBarItemText; }
            set
            {
                if (removeUserAppBarItemText != value)
                {
                    removeUserAppBarItemText = value;
                    NotifyOfPropertyChange(() => RemoveUserAppBarItemText);
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
