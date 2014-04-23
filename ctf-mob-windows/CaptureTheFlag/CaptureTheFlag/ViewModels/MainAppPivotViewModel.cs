using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using CaptureTheFlag.ViewModels.GameVVMs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CaptureTheFlag.ViewModels
{
    public class MainAppPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly ListGamesViewModel listGamesViewModel;
        private readonly IGlobalStorageService globalStorageService;

        ICollection<IScreen> allItems;

        public MainAppPivotViewModel(ListGamesViewModel listGamesViewModel, IGlobalStorageService globalStorageService)
        {
            this.listGamesViewModel = listGamesViewModel;
            this.globalStorageService = globalStorageService;
            allItems = new Collection<IScreen>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(listGamesViewModel);

            globalStorageService.Current.Token = Token;

            foreach (var item in Items)
            {
                allItems.Add(item);
            }

            ActivateItem(listGamesViewModel);
        }

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
    }
}
