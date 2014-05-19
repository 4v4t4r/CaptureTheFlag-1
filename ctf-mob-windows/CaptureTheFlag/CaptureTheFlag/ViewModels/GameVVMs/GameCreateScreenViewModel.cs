using Caliburn.Micro;
using CaptureTheFlag.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameCreateScreenViewModel : PreGameBaseScreenViewModel
    {
        public readonly GameCreateAppBarViewModel gameCreateAppBarViewModel;
        public readonly GlobalStorageService globalStorageService;

        public GameCreateScreenViewModel(GameFieldsViewModel gameFieldsViewModel, GameCreateAppBarViewModel gameCreateAppBarViewModel, IEventAggregator eventAggregator, GlobalStorageService globalStorageService)
            : base(gameFieldsViewModel, eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.gameCreateAppBarViewModel = gameCreateAppBarViewModel;

            DisplayName = "Game edit";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            Items.Add(gameCreateAppBarViewModel);
            gameFieldsViewModel.Game.Url = "TemporaryModelKey";
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            if (globalStorageService.Current.Games.ContainsKey(GameModelKey))
            {
                GameFieldsViewModel.Game = globalStorageService.Current.Games[GameModelKey];
            }
        }

        public GameCreateAppBarViewModel GameCreateAppBarViewModel
        {
            get { return gameCreateAppBarViewModel; }
            set { }
        }
    }
}
