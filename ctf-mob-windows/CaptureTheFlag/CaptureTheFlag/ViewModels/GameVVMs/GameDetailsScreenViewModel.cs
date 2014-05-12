namespace CaptureTheFlag.ViewModels.GameVVMs
{
    using Caliburn.Micro;

    public class GameDetailsScreenViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private readonly GameFieldsViewModel gameFieldsViewModel;
        private readonly GameEditAppBarViewModel gameEditAppBarViewModel;

        public GameDetailsScreenViewModel(GameFieldsViewModel gameFieldsViewModel, GameEditAppBarViewModel gameEditAppBarViewModel)
        {
            this.gameFieldsViewModel = gameFieldsViewModel;
            this.gameEditAppBarViewModel = gameEditAppBarViewModel;

            DisplayName = "Game details";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Items.Add(gameFieldsViewModel);
            Items.Add(gameEditAppBarViewModel);
        }

        public GameFieldsViewModel GameFieldsViewModel
        {
            get { return gameFieldsViewModel; }
            set { }
        }

        public GameEditAppBarViewModel GameEditAppBarViewModel
        {
            get { return gameEditAppBarViewModel; }
            set { }
        }
    }
}
