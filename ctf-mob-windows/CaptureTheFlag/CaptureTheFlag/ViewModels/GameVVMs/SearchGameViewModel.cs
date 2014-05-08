namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using RestSharp;
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;

    public class SearchGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IGlobalStorageService globalStorageService;
        private readonly ICommunicationService communicationService;
        private readonly IFilterService filterService;
        private RestRequestAsyncHandle requestHandle; //TODO: use requestHandle to abort when neccessary

        private BindableCollection<GameD> allGames;

        public SearchGameViewModel(INavigationService navigationService, IGlobalStorageService globalStorageService, IFilterService filterService, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.globalStorageService = globalStorageService;
            this.filterService = filterService;
            this.communicationService = communicationService;

            IsFormAccessible = true;

            Games = new BindableCollection<GameD>();
            Authenticator = new Authenticator();

            DisplayName = "Search games";
        }

        #region Screen states
        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Authenticator = globalStorageService.Current.Authenticator;
            if (globalStorageService.Current.GamesD != null && globalStorageService.Current.GamesD.Count > 0)
            {
                foreach (GameD game in globalStorageService.Current.GamesD.Values)
                {
                    Games.Add(game);
                }
                allGames = Games;
            }
            else
            {
                ListGamesAction();
            }
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            Games.Clear();
            base.OnDeactivate(close);
        }
        #endregion

        #region Helpers
        public async void FilterGames()
        {
            if (!String.IsNullOrWhiteSpace(searchTextBoxText) && !String.IsNullOrEmpty(searchTextBoxText))
            {
                Task<BindableCollection<GameD>> t = filterService.FilterCollectionAsync(allGames, () => Games[0].name, new Regex(searchTextBoxText, RegexOptions.IgnoreCase | RegexOptions.Singleline));
                BindableCollection<GameD> g = await t;
                if (!t.IsCanceled)
                {
                    Games = g;
                }
            }
            else
            {
                Games = allGames;
            }
        }
        #endregion

        #region Actions
        public void ChooseGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (SelectedGame != null && Authenticator.IsValid(Authenticator))
            {
                if (SelectedGame.url == Authenticator.user)
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

        public void ListGamesAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                requestHandle = communicationService.GetAllGames(Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        Games = responseData;
                        foreach (GameD game in Games)
                        {
                            if (!globalStorageService.Current.GamesD.ContainsKey(game.url))
                            {
                                globalStorageService.Current.GamesD[game.url] = game;
                            }
                        }
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

        private BindableCollection<GameD> games;
        public BindableCollection<GameD> Games
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

        private GameD selectedGame;
        public GameD SelectedGame
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
        #endregion

        #region UI Properties

        private string searchTextBoxText;
        public string SearchTextBoxText
        {
            get { return searchTextBoxText; }
            set
            {
                if (searchTextBoxText != value)
                {
                    searchTextBoxText = value;

                    NotifyOfPropertyChange(() => SearchTextBoxText);

                    FilterGames();
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
