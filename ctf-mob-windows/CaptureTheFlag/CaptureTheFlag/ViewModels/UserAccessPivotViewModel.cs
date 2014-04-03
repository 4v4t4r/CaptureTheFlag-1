using Caliburn.Micro;

namespace CaptureTheFlag.ViewModels
{
    public class UserAccessPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly UserLoginViewModel userLoginModelView;
        private readonly UserRegistrationViewModel userRegistrationViewModel;
        
        public UserAccessPivotViewModel(UserLoginViewModel userLoginModelView, UserRegistrationViewModel userRegistrationViewModel)
        {
            this.userLoginModelView = userLoginModelView;
            this.userRegistrationViewModel = userRegistrationViewModel;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(userLoginModelView);
            Items.Add(userRegistrationViewModel);

            ActivateItem(userLoginModelView);
        }
    }
}
