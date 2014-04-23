using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameMapVVMs;
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

namespace CaptureTheFlag.ViewModels.GameMapVVMs
{
    public class SearchGameMapViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IGlobalStorageService globalStorageService;
        private readonly IFilterService filterService;

        private BindableCollection<GameMap> allGameMaps;

        public SearchGameMapViewModel(INavigationService navigationService, IGlobalStorageService globalStorageService, IFilterService filterService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.globalStorageService = globalStorageService;
            this.filterService = filterService;

            GameMaps = new BindableCollection<GameMap>();

            DisplayName = "Search maps";
        }

        #region Screen states
        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (globalStorageService.Current.GameMaps != null)
            {
                foreach (GameMap map in globalStorageService.Current.GameMaps.Values)
                {
                    GameMaps.Add(map);
                }
            }
            allGameMaps = GameMaps;
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            GameMaps.Clear();
            base.OnDeactivate(close);
        }
        #endregion

        #region Helpers
        public async void FilterGames()
        {
            if (!String.IsNullOrWhiteSpace(searchTextBoxText) && !String.IsNullOrEmpty(searchTextBoxText))
            {
                Task<BindableCollection<GameMap>> t = filterService.FilterCollectionAsync(allGameMaps, () => GameMaps[0].name, new Regex(searchTextBoxText, RegexOptions.IgnoreCase | RegexOptions.Singleline));
                BindableCollection<GameMap> g = await t;
                if (!t.IsCanceled)
                {
                    GameMaps = g;
                }
            }
            else
            {
                GameMaps = allGameMaps;
            }
        }
        #endregion

        #region Actions
        public void ChooseGameAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            if (SelectedGameMap != null)
            {
//#warning Cannot determine map's owner
//                if (SelectedGameMap.name == "CTF first test game") //if is owner
//                {
//                    navigationService.UriFor<EditGameViewModel>()
//                         .WithParam(param => param.GameModelKey, SelectedGameMap.url)
//                         .Navigate();
//                }
//                else
//                {
                    navigationService.UriFor<ShowGameMapViewModel>()
                         .WithParam(param => param.GameMapModelKey, SelectedGameMap.url)
                         .Navigate();
                //}
                SelectedGameMap = null;
            }
        }
        #endregion

        #region Properties

        #region Model Properties
        private BindableCollection<GameMap> gameMaps;
        public BindableCollection<GameMap> GameMaps
        {
            get { return gameMaps; }
            set
            {
                if (gameMaps != value)
                {
                    gameMaps = value;
                    NotifyOfPropertyChange(() => GameMaps);
                }
            }
        }

        private GameMap selectedGameMap;
        public GameMap SelectedGameMap
        {
            get { return selectedGameMap; }
            set
            {
                if (selectedGameMap != value)
                {
                    selectedGameMap = value;
                    NotifyOfPropertyChange(() => SelectedGameMap);
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
