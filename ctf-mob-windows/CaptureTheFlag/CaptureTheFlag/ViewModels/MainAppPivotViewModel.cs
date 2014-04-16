using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CaptureTheFlag.ViewModels
{
    public class MainAppPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly CreateMapViewModel createMapViewModel;
        private readonly ListGamesViewModel listGamesViewModel;
        private readonly GameItemViewModel gameItemViewModel;
        private readonly CharacterViewModel characterViewModel;
        private readonly UserViewModel userViewModel;

        ICollection<IScreen> allItems;

        public MainAppPivotViewModel(CreateMapViewModel createMapViewModel, ListGamesViewModel listGamesViewModel, GameItemViewModel gameItemViewModel, CharacterViewModel characterViewModel, UserViewModel userViewModel)
        {
            this.createMapViewModel = createMapViewModel;
            this.listGamesViewModel = listGamesViewModel;
            this.gameItemViewModel = gameItemViewModel;
            this.characterViewModel = characterViewModel;
            this.userViewModel = userViewModel;
            allItems = new Collection<IScreen>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(createMapViewModel);
            Items.Add(listGamesViewModel);
            Items.Add(gameItemViewModel);
            Items.Add(characterViewModel);
            Items.Add(userViewModel);

            //TODO: Find a good way to share a token
            createMapViewModel.Token = Token;
            listGamesViewModel.Token = Token;
            gameItemViewModel.Token = Token;
            characterViewModel.Token = Token;
            userViewModel.Token = Token;

            foreach (var item in Items)
            {
                allItems.Add(item);
            }

            //TODO: Find a good way send url to inactive VM in the same pivot

            ActivateItem(userViewModel);
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
