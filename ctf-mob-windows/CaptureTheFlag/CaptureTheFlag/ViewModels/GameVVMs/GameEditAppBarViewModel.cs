﻿    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Windows;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameEditAppBarViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public GameEditAppBarViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            Game = new Game();

            //TODO: Implement can execute for actions
            UpdateAppBarItemText = "update";
            UpdateIcon = new Uri("/Images/upload.png", UriKind.Relative);

            UpdateSelectiveAppBarItemText = "selective update";
            UpdateSelectiveIcon = new Uri("/Images/share.png", UriKind.Relative);

            DeleteAppBarMenuItemText = "delete";

            IsFormAccessible = true;
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            Game.Url = GameModelKey;

            if (!String.IsNullOrEmpty(Game.Url))
            {
                if (globalStorageService.Current.Games.ContainsKey(Game.Url))
                {
                    Game = globalStorageService.Current.Games[Game.Url];
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
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.ReadGame(Game, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Game = responseData;
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void DeleteAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.DeleteGame(Game, Authenticator.token,
                    responseGameMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        IsFormAccessible = true;
                        if(navigationService.CanGoBack)
                        {
                            navigationService.GoBack();
                            navigationService.RemoveBackEntry();
                        }
                        MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void UpdateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.UpdateGame(Game, Authenticator.token,
                    responseGameMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Game = responseGameMap;
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "Updated", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void UpdateSelectiveAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                Game selectedFields = Game;
                requestHandle = communicationService.UpdateGameFields(selectedFields, Authenticator.token,
                    responseGameMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Game = responseGameMap;
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "Updated", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }
        #endregion

        #region Properties

        #region Model Properties
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
        #endregion

        #region UI Properties
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

        //TODO: remove when can execute is available
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