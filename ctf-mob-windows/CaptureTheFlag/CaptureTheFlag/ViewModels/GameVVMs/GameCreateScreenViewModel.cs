using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.ViewModels.GameVVMs
{
    public class GameCreateScreenViewModel : PreGameBaseScreenViewModel
    {
        public GameCreateScreenViewModel(GameFieldsViewModel gameFieldsViewModel, IEventAggregator eventAggregator)
            : base(gameFieldsViewModel, eventAggregator)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "");
        }
    }
}
