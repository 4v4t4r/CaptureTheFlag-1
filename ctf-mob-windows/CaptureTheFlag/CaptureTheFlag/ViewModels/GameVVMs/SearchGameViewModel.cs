using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameVVMs;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class SearchGameViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IGlobalStorageService globalStorageService;
        private readonly IFilterService filterService;

        private BindableCollection<Game> allGames;

        public SearchGameViewModel(INavigationService navigationService, IGlobalStorageService globalStorageService, IFilterService filterService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.globalStorageService = globalStorageService;
            this.filterService = filterService;

            Games = new BindableCollection<Game>();

            DisplayName = "Search games";
        }

        #region Screen states
        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (globalStorageService.Current.Games != null)
            {
                foreach (Game game in globalStorageService.Current.Games.Values)
                {
                    Games.Add(game);
                }
            }
            allGames = Games;
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
                Task<BindableCollection<Game>> t = filterService.FilterCollectionAsync(allGames, () => Games[0].name, new Regex(searchTextBoxText, RegexOptions.IgnoreCase | RegexOptions.Singleline));
                BindableCollection<Game> g = await t;
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
            if (SelectedGame != null)
            {
#warning Cannot determine game's owner
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
        #endregion

        #region Properties

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
        #endregion
        #endregion
    }
}
