using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CaptureTheFlag.ViewModels
{
    public class UserAccessPivotViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<bool>
    {
        private readonly UserLoginViewModel userLoginModelView;
        private readonly UserRegistrationViewModel userRegistrationViewModel;
        private readonly IEventAggregator eventAggregator;
        ICollection<IScreen> allItems;
        
        public UserAccessPivotViewModel(IEventAggregator eventAggregator, UserLoginViewModel userLoginModelView, UserRegistrationViewModel userRegistrationViewModel)
        {
            this.eventAggregator = eventAggregator;
            this.userLoginModelView = userLoginModelView;
            this.userRegistrationViewModel = userRegistrationViewModel;
            allItems = new Collection<IScreen>();
        }

        #region ViewModel States
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(userLoginModelView);
            Items.Add(userRegistrationViewModel);
            foreach (var item in Items)
            {
                allItems.Add(item);
            }

            ActivateItem(userLoginModelView);

            INavigationService navigationService = IoC.Get<INavigationService>();
            navigationService
                .UriFor<MainAppPivotViewModel>()
                .WithParam(param => param.Token, "faef06263daf128be3accaf2089dbc12f9251c1e")
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        #region Message Handling
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
