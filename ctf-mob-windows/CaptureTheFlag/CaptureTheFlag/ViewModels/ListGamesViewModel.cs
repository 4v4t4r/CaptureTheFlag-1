using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class ListGamesViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;

        public ListGamesViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            Games = new BindableCollection<Game>();

            DisplayName = "Games list";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            if(close)
            {
                eventAggregator.Unsubscribe(this);
            }
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void GamesListAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            //communicationService.GetAllGames(Token,
            //    responseData =>
            //    {
            //        DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
            //        //eventAggregator.Publish(Game);
            //        Games = responseData;
            //    },
            //    serverErrorMessage =>
            //    {
            //        DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
            //        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
            //    }
            //);
            SelectedGame = new Game();
            //SelectedGame.type = 1;
            //SelectedGame.name = "Asg game name";
            //SelectedGame.description = "asasd description";
            //SelectedGame.action_range = 20.0f;
            //SelectedGame.visibility_range = 100.0f;
            //SelectedGame.max_players = 10;
            //SelectedGame.map = "http://78.133.154.39:8888/api/maps/13/";
            SelectedGame.url = "http://78.133.154.39:8888/api/games/23/";

            //IoC.Get<GlobalStorageService>().Current.Games[SelectedGame.url] = SelectedGame;

            navigationService.UriFor<CreateGameViewModel>()
                 .WithParam(param => param.GameModelKey, SelectedGame.url)
                 .WithParam(param => param.Token, Token)
                 .Navigate();
        }

        public void ReadGameAction()
        {
            navigationService.UriFor<CreateGameViewModel>().WithParam(param => param.Token, Token).Navigate();
            eventAggregator.Publish(SelectedGame);
            //SelectedGame = null;
        }

        public void AddToGameAction()
        {

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
        #endregion

        #endregion
    }
}
