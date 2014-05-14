using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace PushNotificationWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg;
            if (NavigationContext.QueryString.TryGetValue("JsonData", out msg))
            {
                System.Diagnostics.Debug.WriteLine("ReceivedToastNotificationAndNavigated");
                System.Diagnostics.Debug.WriteLine("{0}", msg);
            }

            RegisterNotificationChannel();
        }

        public static HttpNotificationChannel pushChannel = null;

        public void RegisterNotificationChannel()
        {
            string channelName = "NotificationExample";
            pushChannel = HttpNotificationChannel.Find(channelName);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(PushChannel_HttpNotificationReceived);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(PushChannel_HttpNotificationReceived);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", pushChannel.ChannelUri.ToString()));
            }
        }

        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ShellToastNotificationReceived");
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            // Display a dialog of all the fields in the toast.
            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                System.Diagnostics.Debug.WriteLine(message.ToString());
            });
        }

        void PushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HttpNotificationReceived");
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var sr = new StreamReader(e.Notification.Body);
                var myStr = sr.ReadToEnd();
                MessageBox.Show(myStr);
                System.Diagnostics.Debug.WriteLine(myStr);
            });
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ErrorOccurred");
            // Error handling logic for your particular application would be here.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}", e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
                System.Diagnostics.Debug.WriteLine(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}", e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
            });
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ChannelUriUpdated");
            // Display the new URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(String.Format("Channel Uri is {0}", e.ChannelUri.ToString()));
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
            });
        }
    }
}