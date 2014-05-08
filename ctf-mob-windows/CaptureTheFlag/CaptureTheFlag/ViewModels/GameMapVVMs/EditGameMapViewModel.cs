using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameVVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels.GameMapVVMs
{

    public class EditGameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;

        public EditGameMapViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            IsFormAccessible = true;

            GameMap = new GameMap();
            User = new UserD();
            //TODO: update to model probably

            DisplayName = "Map Edit";

            UpdateAppBarItemText = "update";
            UpdateIcon = new Uri("/Images/upload.png", UriKind.Relative);

            UpdateSelectiveAppBarItemText = "selective update";
            UpdateSelectiveIcon = new Uri("/Images/share.png", UriKind.Relative);

            DeleteAppBarMenuItemText = "delete";

            AuthorTextBlock = "Author:";
            NameTextBlock = "Name:";
            DescriptionTextBlock = "Description:";
            RadiusTextBlock = "Radius:";
            LatTextBlock = "Latitude:";
            LonTextBlock = "Longitude:";
            GamesTextBlock = "Games:";

            ChooseMapAppBarItemText = "choose";
            ChooseMapIcon = new Uri("/Images/check.png", UriKind.Relative);
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            GameMap.url = GameMapModelKey;

            if (!String.IsNullOrEmpty(GameMap.url))
            {
                if (globalStorageService.Current.GamesD.ContainsKey(GameMap.url))
                {
                    GameMap = globalStorageService.Current.GameMaps[GameMap.url];
                }
                else
                {
                    ReadGameMapAction();
                }
            }
        }
        #endregion

        #region Actions
        public void ReadGameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.ReadGameMap(GameMap, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        GameMap = responseData;
                        User.url = GameMap.author;
                        ReadUserAction();
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void ReadUserAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.ReadUser(User, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        User = responseData;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        //public void ChooseMapAction()
        //{
        //    globalStorageService.Current.GameMaps[GameMap.url] = GameMap;
        //    navigationService.UriFor<CreateGameViewModel>()
        //        .WithParam(param => param.GameMapModelKey, GameMap.url)
        //        .Navigate();
        //}

        public void UpdateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            string token = globalStorageService.Current.Token;
            if (!String.IsNullOrEmpty(token))
            {
                communicationService.UpdateGameMap(GameMap, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        GameMap = responseData;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
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
                communicationService.UpdateGameMapFields(GameMap, token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        GameMap = responseData;
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

        private UserD user;
        public UserD User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    NotifyOfPropertyChange(() => User);
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
        #endregion

        #region UI Properties
        private string authorTextBlock;
        public string AuthorTextBlock
        {
            get { return authorTextBlock; }
            set
            {
                if (authorTextBlock != value)
                {
                    authorTextBlock = value;
                    NotifyOfPropertyChange(() => AuthorTextBlock);
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

        private string gamesTextBlock;
        public string GamesTextBlock
        {
            get { return gamesTextBlock; }
            set
            {
                if (gamesTextBlock != value)
                {
                    gamesTextBlock = value;
                    NotifyOfPropertyChange(() => GamesTextBlock);
                }
            }
        }

        private Uri chooseMapIcon;
        public Uri ChooseMapIcon
        {
            get { return chooseMapIcon; }
            set
            {
                if (chooseMapIcon != value)
                {
                    chooseMapIcon = value;
                    NotifyOfPropertyChange(() => ChooseMapIcon);
                }
            }
        }

        private string chooseMapAppBarItemText;
        public string ChooseMapAppBarItemText
        {
            get { return chooseMapAppBarItemText; }
            set
            {
                if (chooseMapAppBarItemText != value)
                {
                    chooseMapAppBarItemText = value;
                    NotifyOfPropertyChange(() => ChooseMapAppBarItemText);
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
