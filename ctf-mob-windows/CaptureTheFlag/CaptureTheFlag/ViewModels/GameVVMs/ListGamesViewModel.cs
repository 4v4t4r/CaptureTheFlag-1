using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameMapVVMs;
using CaptureTheFlag.ViewModels.GameVVMs;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class ListGamesViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly ILocationService locationService; //TODO: move
        private readonly IGlobalStorageService globalStorageService;

        public ListGamesViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService, ILocationService locationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;

            Games = new BindableCollection<Game>();

            DisplayName = "Games";

            FindAppBarItemText = "find";
            FindIcon = new Uri("/Images/feature.search.png", UriKind.Relative);
            CreateAppBarItemText = "create";
            CreateIcon = new Uri("/Images/add.png", UriKind.Relative);
            RefreshAppBarItemText = "refresh";
            RefreshIcon = new Uri("/Images/refresh.png", UriKind.Relative);

            SettingsAppBarMenuItemText = "settings";
            CharactersAppBarMenuItemText = "characters";
            ProfileAppBarMenuItemText = "profile";

            ShouldEdit = false;
        }

        #region Watcher TODO: move
        private GeoCoordinateWatcher watcher;
        public GeoCoordinateWatcher Watcher
        {
            get { return watcher; }
            set
            {
                if (watcher != value)
                {
                    watcher = value;
                    NotifyOfPropertyChange(() => Watcher);
                }
            }
        }
        public void RegisterPositionAction()
        {
            //TODO: Response object model
            Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            Watcher.Start();
            communicationService.RegisterPosition(Games.FirstOrDefault(), Watcher.Position.Location, Token,
                rData =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                }
            );
            Watcher.Stop();
        }
        #endregion


        protected override void OnActivate()
        {
            base.OnActivate();
            if(String.IsNullOrEmpty(Token))
            {
                Token = globalStorageService.Current.Token;
            }
        }

        #region Actions
        public void ListGamesAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.GetAllGames(Token,
                responseData =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    Games = responseData;
                    foreach(Game game in Games)
                    {
                        if (!globalStorageService.Current.Games.ContainsKey(game.url))
                        {
                            globalStorageService.Current.Games[game.url] = game;
                        }
                    }
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void ReadGameAction()
        {
            if (SelectedGame != null)
            {
                if (SelectedGame.name == "CTF first test game") //if is owner
                {
                    navigationService.UriFor<EditGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.url)
                         .Navigate();
                }
                else
                {
                    navigationService.UriFor<ShowGameViewModel>()
                         .WithParam(param => param.GameModelKey, SelectedGame.url)
                         .Navigate();
                }
                SelectedGame = null;
            }
        }

        public void FindGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<SearchGameViewModel>().Navigate();
        }

        public void CreateGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<CreateGameViewModel>().Navigate();
        }

        public void RefreshGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            ListGamesAction();
        }

        public void CharactersAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        }

        public void ProfileAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        }

        public void SettingsAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        }
        #endregion

        #region Properties
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

        #region Model Properties
        private BindableCollection<Game> games;
        public BindableCollection<Game> Games
        {
            get { return games; }
            set
            {
                if (games != value)
                {
                    games = value;
                    NotifyOfPropertyChange(() => Games);
                }
            }
        }

        private Game selectedGame;
        public Game SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (selectedGame != value)
                {
                    selectedGame = value;
                    NotifyOfPropertyChange(() => SelectedGame);
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

        private bool shouldEdit;
        public bool ShouldEdit
        {
            get { return shouldEdit; }
            set
            {
                if (shouldEdit != value)
                {
                    shouldEdit = value;
                    NotifyOfPropertyChange(() => ShouldEdit);
                }
            }
        }
        #endregion

        #region UI properties
        private Uri findIcon;
        public Uri FindIcon
        {
            get { return findIcon; }
            set
            {
                if (findIcon != value)
                {
                    findIcon = value;
                    NotifyOfPropertyChange(() => FindIcon);
                }
            }
        }

        private Uri createIcon;
        public Uri CreateIcon
        {
            get { return createIcon; }
            set
            {
                if (createIcon != value)
                {
                    createIcon = value;
                    NotifyOfPropertyChange(() => CreateIcon);
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

        private string findAppBarItemText;
        public string FindAppBarItemText
        {
            get { return findAppBarItemText; }
            set
            {
                if (findAppBarItemText != value)
                {
                    findAppBarItemText = value;
                    NotifyOfPropertyChange(() => FindAppBarItemText);
                }
            }
        }

        private string createAppBarItemText;
        public string CreateAppBarItemText
        {
            get { return createAppBarItemText; }
            set
            {
                if (createAppBarItemText != value)
                {
                    createAppBarItemText = value;
                    NotifyOfPropertyChange(() => CreateAppBarItemText);
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

        private string charactersAppBarMenuItemText;
        public string CharactersAppBarMenuItemText
        {
            get { return charactersAppBarMenuItemText; }
            set
            {
                if (charactersAppBarMenuItemText != value)
                {
                    charactersAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => CharactersAppBarMenuItemText);
                }
            }
        }

        private string profileAppBarMenuItemText;
        public string ProfileAppBarMenuItemText
        {
            get { return profileAppBarMenuItemText; }
            set
            {
                if (profileAppBarMenuItemText != value)
                {
                    profileAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => ProfileAppBarMenuItemText);
                }
            }
        }

        private string settingsAppBarMenuItemText;
        public string SettingsAppBarMenuItemText
        {
            get { return settingsAppBarMenuItemText; }
            set
            {
                if (settingsAppBarMenuItemText != value)
                {
                    settingsAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => SettingsAppBarMenuItemText);
                }
            }
        }
        #endregion

        #endregion
    }
}
