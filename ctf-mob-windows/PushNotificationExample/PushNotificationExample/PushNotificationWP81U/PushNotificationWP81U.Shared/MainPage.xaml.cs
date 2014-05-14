using System;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PushNotificationWP81U
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //string msg;
            //if (NavigationContext.QueryString.TryGetValue("JsonData", out msg))
            //{
            //    System.Diagnostics.Debug.WriteLine("{0}", msg);
            //}

            RegisterNotificationChannel();
        }

        private PushNotificationChannel pushNotificationChannel = null;

        private async void RegisterNotificationChannel()
        {
            pushNotificationChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            System.Diagnostics.Debug.WriteLine(pushNotificationChannel.Uri);
            pushNotificationChannel.PushNotificationReceived += pushNotificationChannel_PushNotificationReceived;
        }

        private void pushNotificationChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            switch (args.NotificationType)
            {
                case PushNotificationType.Badge:
                    //Not used in sample, but usefull to process incoming badge notification messages
                    //notificationContent = args.BadgeNotification.Content.GetXml(); 
                    //...
                    break;
                case PushNotificationType.Tile:
                    //Not used in sample, but usefull to process incoming tile notification messages
                    //notificationContent = args.TileNotification.Content.GetXml();
                    //...
                    break;
                case PushNotificationType.Toast:
                    //Not used in sample, but usefull to process incoming toast notification messages
                    //notificationContent = args.ToastNotification.Content.GetXml();

                    //...
                    ToastNotificationManager.CreateToastNotifier().Show(args.ToastNotification);
                    System.Diagnostics.Debug.WriteLine(args.ToastNotification.Content);
                    break;
                case PushNotificationType.Raw:
                    System.Diagnostics.Debug.WriteLine(args.RawNotification.Content);
                    break;
            }
            // prevent the notification from being delivered to the UI
            args.Cancel = true;
        }
    }
}
