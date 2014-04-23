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

namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    public class ListGameMapsViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly ILocationService locationService; //TODO: move
        private readonly IGlobalStorageService globalStorageService;

        public ListGameMapsViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService, ILocationService locationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.locationService = locationService;
            this.globalStorageService = globalStorageService;

            Maps = new BindableCollection<GameMap>();

            DisplayName = "Maps";

            FindAppBarItemText = "find";
            FindIcon = new Uri("/Images/feature.search.png", UriKind.Relative);
            CreateAppBarItemText = "create";
            CreateIcon = new Uri("/Images/add.png", UriKind.Relative);
            RefreshAppBarItemText = "refresh";
            RefreshIcon = new Uri("/Images/refresh.png", UriKind.Relative);

            ShouldEdit = false;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (String.IsNullOrEmpty(Token))
            {
                Token = globalStorageService.Current.Token;
            }
        }

        #region Actions
        public void ListMapsAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.GetAllMaps(Token,
                responseData =>
                {
                    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                    Maps = responseData;
                    foreach (GameMap map in Maps)
                    {
                        if (!globalStorageService.Current.GameMaps.ContainsKey(map.url))
                        {
                            globalStorageService.Current.GameMaps[map.url] = map;
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

        public void ReadMapAction()
        {
            if (SelectedMap != null)
            {
                if (true) //if is owner
                {
                    navigationService.UriFor<EditGameMapViewModel>()
                         .WithParam(param => param.GameMapModelKey, SelectedMap.url)
                         .Navigate();
                }
                else
                {
                    navigationService.UriFor<ShowGameMapViewModel>()
                         .WithParam(param => param.GameMapModelKey, SelectedMap.url)
                         .Navigate();
                }
                SelectedMap = null;
            }
        }

        public void SearchGameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<SearchGameMapViewModel>().Navigate();
        }

        public void CreateGameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<CreateGameMapViewModel>().Navigate();
        }

        public void RefreshGameMapAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            ListMapsAction();
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
        private BindableCollection<GameMap> maps;
        public BindableCollection<GameMap> Maps
        {
            get { return maps; }
            set
            {
                if (maps != value)
                {
                    maps = value;
                    NotifyOfPropertyChange(() => Maps);
                }
            }
        }

        private GameMap selectedMap;
        public GameMap SelectedMap
        {
            get { return selectedMap; }
            set
            {
                if (selectedMap != value)
                {
                    selectedMap = value;
                    NotifyOfPropertyChange(() => SelectedMap);
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
        #endregion

        #endregion
    }
}
