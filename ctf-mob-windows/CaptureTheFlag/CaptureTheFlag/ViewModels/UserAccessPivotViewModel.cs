namespace CaptureTheFlag.ViewModels
{
    using Caliburn.Micro;
    using CaptureTheFlag.Models;
    using CaptureTheFlag.Services;
    using CaptureTheFlag.ViewModels.GameMapVVMs;
    using CaptureTheFlag.ViewModels.GameVVMs;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class UserAccessPivotViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<bool>
    {
        private readonly UserLoginViewModel userLoginModelView;
        private readonly UserRegistrationViewModel userRegistrationViewModel;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private readonly IGlobalStorageService globalStorageService;
        ICollection<IScreen> allItems;

        public UserAccessPivotViewModel(IEventAggregator eventAggregator, UserLoginViewModel userLoginModelView, UserRegistrationViewModel userRegistrationViewModel, INavigationService navigationService, IGlobalStorageService globalStorageService)
        {
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
            this.globalStorageService = globalStorageService;
            this.userLoginModelView = userLoginModelView;
            this.userRegistrationViewModel = userRegistrationViewModel;
            allItems = new Collection<IScreen>();

            Items.Add(userLoginModelView);
            Items.Add(userRegistrationViewModel);
            foreach (var item in Items)
            {
                allItems.Add(item);
            }
        }

        #region ViewModel States
        protected override void OnInitialize()
        {
            base.OnInitialize();

            //TODO: Remove when saving to settings implemented
#warning temporary code region
            #region Temporary code
            Authenticator authenticator = new Authenticator();
            authenticator.token = "6681fba5e2190a32d993e194ab2628ab29da7205";
            authenticator.user = "http://78.133.154.39:8888/api/users/5/";
            globalStorageService.Current.Authenticator = authenticator;
            #endregion

            Authenticator authenticatorStored = globalStorageService.Current.Authenticator;
            if (Authenticator.IsValid(authenticatorStored))
            {
                navigationService
                    .UriFor<MainAppPivotViewModel>()
                    .Navigate();
                navigationService.RemoveBackEntry();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            eventAggregator.Subscribe(this);
            ActivateItem(userLoginModelView);
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Message Handling
        //TODO: Consider caliburn micro "CanExecute" functionality
        public void Handle(bool areFormsAccessible)
        {
            if (areFormsAccessible)
            {
                Items.Clear();
                foreach (IScreen item in allItems)
                {
                    Items.Add(item);
                }
            }
            else
            {
                IScreen activeItem = ActiveItem;
                Items.Clear();
                Items.Add(activeItem);
            }
        }
        #endregion
    }
}
