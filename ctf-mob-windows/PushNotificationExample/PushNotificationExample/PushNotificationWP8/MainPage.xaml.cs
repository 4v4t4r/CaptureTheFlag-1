using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using System;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace PushNotificationWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static PushNotificationService pushNotificationService;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            RegisterNotificationChannel();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if(pushChannel != null)
            {
                pushChannel.Close();
            }
        }

        public static HttpNotificationChannel pushChannel = null;

        public void RegisterNotificationChannel()
        {
            string channelName = "RawNotificationExample";
            pushChannel = HttpNotificationChannel.Find(channelName);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(PushChannel_HttpNotificationReceived);

                pushChannel.Open();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(PushChannel_HttpNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", pushChannel.ChannelUri.ToString()));
            }
        }

        void PushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("http");
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var sr = new StreamReader(e.Notification.Body);
                var myStr = sr.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(myStr);
            });
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("error");
            // Error handling logic for your particular application would be here.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}", e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
            });
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            // Display the new URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", e.ChannelUri.ToString()));
            });
        }
    }
}