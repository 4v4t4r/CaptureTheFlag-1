namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Messages;
    using System.Reflection;

    public class GameDetailsScreenViewModel : PreGameBaseScreenViewModel
    {
        private readonly GameDetailsAppBarViewModel gameDetailsAppBarViewModel;

        public GameDetailsScreenViewModel(GameFieldsViewModel gameFieldsViewModel, GameDetailsAppBarViewModel gameDetailsAppBarViewModel, IEventAggregator eventAggregator)
            : base(gameFieldsViewModel, eventAggregator) //TODO: Remove requests for GameField and EventAgregator
        {
            this.gameDetailsAppBarViewModel = gameDetailsAppBarViewModel;

            DisplayName = "Game details";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            Items.Add(gameDetailsAppBarViewModel);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            base.eventAggregator.Publish(new GameModelMessage() { GameModelKey = GameModelKey, Status = ModelMessage.STATUS.SHOULD_GET });
        }

        public GameDetailsAppBarViewModel GameDetailsAppBarViewModel
        {
            get { return gameDetailsAppBarViewModel; }
            set { }
        }
    }
}
