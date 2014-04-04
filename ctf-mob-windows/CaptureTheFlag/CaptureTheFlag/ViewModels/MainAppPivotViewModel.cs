using Caliburn.Micro;

namespace CaptureTheFlag.ViewModels
{
    public class MainAppPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public MainAppPivotViewModel()
        {

        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }


        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyOfPropertyChange("Name");
                }
            }
        }
    }
}
