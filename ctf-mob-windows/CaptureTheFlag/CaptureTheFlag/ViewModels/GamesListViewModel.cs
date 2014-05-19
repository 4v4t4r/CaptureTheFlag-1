using Caliburn.Micro;
using CaptureTheFlag.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.ViewModels
{
    public class GamesListViewModel : Screen, IHandle<BindableCollection<PreGame>>
    {
        private readonly IEventAggregator eventAggregator;

        public GamesListViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Games = new BindableCollection<PreGame>();
            for(int i = 0; i < 10; ++i)
            {
                Games.Add(new PreGame() { Name = String.Format("{0}", i) });
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        private BindableCollection<PreGame> games;
        public BindableCollection<PreGame> Games
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

        private PreGame selectedGame;
        public PreGame SelectedGame
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

        public void OpenAction()
        {
            Debug.WriteLine("OpenAction");
            eventAggregator.Publish(SelectedGame);
        }

        public void Handle(BindableCollection<PreGame> message)
        {
            Games = message;
        }
    }
}
