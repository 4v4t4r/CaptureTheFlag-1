using Microsoft.Phone.Notification;
using System;
using System.IO;
using System.Windows;
namespace CaptureTheFlag.Services
{
    public class PushNotificationService
    {
        private HttpNotificationChannel pushChannel = null;

        //TODO: move to notification service
        #region Push Notifications
        //NOTE: Notification is recieved as HttpNotification regardless it it is toast or not (Toast is recieved in Windows 8.1 Universal App on phone)
        //NOTE: Puah notification channel will be removed by MS when app is deactivated/tombstoned ( http://rarcher.azurewebsites.net/Post/PostContent/34 )
        private void RegisterNotificationChannel()
        {
            string channelName = "ctf/aplha/notification/wp";
            pushChannel = HttpNotificationChannel.Find(channelName);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(pushChannel_HttpNotificationReceived);

                pushChannel.Open();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(pushChannel_HttpNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}",
                    pushChannel.ChannelUri.ToString()));

            }
        }

        void pushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var sr = new StreamReader(e.Notification.Body);
                var myStr = sr.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(myStr);
            });
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}", e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
            });
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", e.ChannelUri.ToString()));
            });
        }
        #endregion
    }
}
