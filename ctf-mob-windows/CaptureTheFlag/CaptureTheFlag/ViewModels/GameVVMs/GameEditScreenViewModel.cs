namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using CaptureTheFlag.Messages;
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
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            eventAggregator.Publish(new GameModelMessage() { GameModelKey = GameModelKey, Status = ModelMessage.STATUS.SHOULD_GET });
        }

        public GameEditAppBarViewModel GameEditAppBarViewModel
        {
            get { return gameEditAppBarViewModel; }
            set { }
        }
    }
}
