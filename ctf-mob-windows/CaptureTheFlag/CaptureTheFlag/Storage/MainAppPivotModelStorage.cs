using Caliburn.Micro;
using CaptureTheFlag.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Storage
{
    public class MainAppPivotModelStorage : StorageHandler<MainAppPivotViewModel>
    {
        public override void Configure()
        {
            Property(x => x.Token)
            .InAppSettings();
        }
    }
}
