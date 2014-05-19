namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Messages;
    using CaptureTheFlag.Models;
    using System.Reflection;

    public class GameEditScreenViewModel : PreGameBaseScreenViewModel
    {
        private readonly GameEditAppBarViewModel gameEditAppBarViewModel;

        public GameEditScreenViewModel(GameFieldsViewModel gameFieldsViewModel, GameEditAppBarViewModel gameEditAppBarViewModel, IEventAggregator eventAggregator)
            : base(gameFieldsViewModel, eventAggregator) //TODO: Remove requests for GameField and EventAgregator
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.gameEditAppBarViewModel = gameEditAppBarViewModel;

            DisplayName = "Game edit";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            Items.Add(gameEditAppBarViewModel);
            GameFieldsViewModel.Game.Url = GameModelKey;
        }

        public GameEditAppBarViewModel GameEditAppBarViewModel
        {
            get { return gameEditAppBarViewModel; }
            set { }
        }
    }
}
