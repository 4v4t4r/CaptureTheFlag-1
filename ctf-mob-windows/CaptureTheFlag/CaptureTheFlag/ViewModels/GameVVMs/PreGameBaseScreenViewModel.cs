namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;
    using System.Reflection;

    public class PreGameBaseScreenViewModel : Conductor<IScreen>.Collection.AllActive
    {
        protected readonly GameFieldsViewModel gameFieldsViewModel;
        protected readonly IEventAggregator eventAggregator;

        public PreGameBaseScreenViewModel(GameFieldsViewModel gameFieldsViewModel, IEventAggregator eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            this.gameFieldsViewModel = gameFieldsViewModel;
            this.eventAggregator = eventAggregator;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
            Items.Add(gameFieldsViewModel);
        }

        public GameFieldsViewModel GameFieldsViewModel
        {
            get { return gameFieldsViewModel; }
            set { }
        }

        private string gameModelKey;
        public string GameModelKey
        {
            get { return gameModelKey; }
            set
            {
                if (gameModelKey != value)
                {
                    gameModelKey = value;
                }
            }
        }
    }
}
