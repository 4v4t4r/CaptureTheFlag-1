using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CaptureTheFlag.Views
{
    public partial class InGameMapView : PhoneApplicationPage
    {
        public InGameMapView()
        {
            InitializeComponent();
        }

        private void Map_ZoomLevelChanged(object sender, Microsoft.Phone.Maps.Controls.MapZoomLevelChangedEventArgs e)
        {

        }
    }
}