namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Windows;
    public class ListGameMapsViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public ListGameMapsViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            Maps = new BindableCollection<GameMap>();
            Authenticator = new Authenticator();

            DisplayName = "Maps";

            FindAppBarItemText = "find";
            FindIcon = new Uri("/Images/feature.search.png", UriKind.Relative);
            CreateAppBarItemText = "create";
            CreateIcon = new Uri("/Images/add.png", UriKind.Relative);
            RefreshAppBarItemText = "refresh";
            RefreshIcon = new Uri("/Images/refresh.png", UriKind.Relative);
        }

        #region Screen states
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            if (globalStorageService.Current.GameMaps != null && globalStorageService.Current.GameMaps.Count > 0)
            {
                foreach (GameMap map in globalStorageService.Current.GameMaps.Values)
                {
                    Maps.Add(map);
                }
            }
            else
            {
                ListMapsAction();
            }
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Maps.Clear();
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void ListMapsAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.GetAllMaps(Authenticator.token,
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

        public void ReadMapAction()
        {
            if (SelectedMap != null)
            {
                //TODO: Make maps editable maybe
                //TODO: if statment should be: SelectedMap.author == Authenticator.user
                if (false)
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
