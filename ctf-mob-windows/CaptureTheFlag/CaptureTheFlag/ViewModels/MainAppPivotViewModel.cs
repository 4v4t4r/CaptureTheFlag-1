using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;

namespace CaptureTheFlag.ViewModels
{
    public class MainAppPivotViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private ICommunicationService communicationService;

        public MainAppPivotViewModel (ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            //communicationService.GetAllGames<ServerErrorMessage>(Token, response => { });
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
