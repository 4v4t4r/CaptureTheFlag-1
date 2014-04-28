namespace CaptureTheFlag.ViewModels
{
    using Caliburn.Micro;
    using CaptureTheFlag.ViewModels.GameVVMs;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainAppPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly ListGamesViewModel listGamesViewModel;

        ICollection<IScreen> allItems;

        public MainAppPivotViewModel(ListGamesViewModel listGamesViewModel)
        {
            this.listGamesViewModel = listGamesViewModel;
            allItems = new Collection<IScreen>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(listGamesViewModel);

            foreach (var item in Items)
            {
                allItems.Add(item);
            }

            ActivateItem(listGamesViewModel);
        }
    }
}
