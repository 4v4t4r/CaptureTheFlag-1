using Caliburn.Micro;
using CaptureTheFlag.ViewModels.GameVVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.ViewModels
{
    public class GamesListScreenViewModel : PreGameBaseScreenViewModel
    {
        private readonly GamesListViewModel gamesListViewModel;
        private readonly GamesListAppBarViewModel gamesListAppBarViewModel;

        public GamesListScreenViewModel(GamesListViewModel gamesListViewModel, GamesListAppBarViewModel gamesListAppBarViewModel, IEventAggregator eventAggregator)
            : base(null, eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.gamesListViewModel = gamesListViewModel;
            this.gamesListAppBarViewModel = gamesListAppBarViewModel;

            DisplayName = "Games list";

            Items.Add(gamesListViewModel);
            Items.Add(gamesListAppBarViewModel);
        }

        public GamesListViewModel GamesListViewModel
        {
            get { return gamesListViewModel; }
            private set { }
        }

        public GamesListAppBarViewModel GamesListAppBarViewModel
        {
            get { return gamesListAppBarViewModel; }
            private set { }
        }
    }
}
