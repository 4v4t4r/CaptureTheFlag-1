using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PushNotificationServer
{
    public sealed partial class MainPage : Page
    {
        //URI should be sent form the app to server
        private static string uri = "https://db3.notify.windows.com/?token=AgYAAAAzFb%2fY64%2bM2SfsOC3fPeCmw5iy9r7oxz%2f7JV2lsaAVErP%2bhzRaGWDihpkgDlbm08vQ0nTdRrnYIl5wdB6jbKkVY7ugACxMegt7o5A1OObnqQFaYrU%2fbjsqvXkg9zQWw6M%3d";

        #region Notifications
        //Reference: http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff402558(v=vs.105).aspx
        //Modified code from: http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202977(v=vs.105).aspx
        //Reference: http://msdn.microsoft.com/en-us/library/windows/apps/hh913756.aspx
        //Modified code from: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868252.aspx
        private async void SendRawNotificationAction(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Uri);

                //WNS and MPNS
                message.Headers.Add("X-WNS-Type", "wns/raw");
                message.Headers.Add("Authorization", String.Format("Bearer {0}", Token.AccessToken));
                message.Headers.Add("X-NotificationClass", "3");

                string contentString = getJsonContent();

                message.Content = ContentStreamFromString(contentString);
                HttpResponseMessage resp = await client.SendAsync(message);
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine("Send toast response status code: {0}", resp.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //Reference: http://msdn.microsoft.com/en-us/library/hh221551.aspx
        private HttpRequestMessage MPNSToastNotificationMessage()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Uri);

            //MPNS only
            message.Headers.Add("X-WindowsPhone-Target", "toast");
            message.Headers.Add("X-NotificationClass", "2");

            string contentString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                       "<wp:Toast>" +
                            "<wp:Text1>" + toastBox.Text.ToString() + "</wp:Text1>" +
                            "<wp:Text2>" + toastBox.Text.ToString() + "</wp:Text2>" +
                            "<wp:Param>/MainPage.xaml?JsonData=" + getJsonContent() + "</wp:Param>" + // MainPage.xaml in the real app will be changed for /Views/PushNotificationView.xaml
                       "</wp:Toast> " +
                    "</wp:Notification>";

            message.Content = ContentStreamFromString(contentString);
            return message;
        }

        //Reference: http://msdn.microsoft.com/en-us/library/windows/apps/br230846.aspx
        //Reference: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868245.aspx
        private HttpRequestMessage LaunchWNSToastNotificationMessage()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Uri);

            //WNS only
            message.Headers.Add("X-WNS-Type", "wns/toast");
            message.Headers.Add("Authorization", String.Format("Bearer {0}", Token.AccessToken));

            string contentString = "<toast launch=\"" + getJsonContent() + "\">" +
                                      "<visual lang=\"en-US\">" +
                                        "<binding template=\"ToastText01\">" +
                                          "<text id=\"1\">" + toastBox.Text.ToString()  + "</text>" +
                                        "</binding>" +
                                      "</visual>" +
                                    "</toast>";

            message.Content = ContentStreamFromString(contentString);
            return message;
        }

        private HttpRequestMessage WNSToastNotificationMessage()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Uri);

            //WNS only
            message.Headers.Add("X-WNS-Type", "wns/toast");
            message.Headers.Add("Authorization", String.Format("Bearer {0}", Token.AccessToken));

            string contentString = "<toast launch=\"\">" +
                                      "<visual lang=\"en-US\">" +
                                        "<binding template=\"ToastText01\">" +
                                          "<text id=\"1\">" + toastBox.Text.ToString() + "</text>" +
                                        "</binding>" +
                                      "</visual>" +
                                    "</toast>";

            message.Content = ContentStreamFromString(contentString);
            return message;
        }

        private async void SendToastNotificationWNSLaunchAction(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage resp = await client.SendAsync(LaunchWNSToastNotificationMessage());
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine("Send toast response status code: {0}", resp.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void SendToastNotificationWNSAction(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage resp = await client.SendAsync(WNSToastNotificationMessage());
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine("Send toast response status code: {0}", resp.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void SendToastNotificationMPNSAction(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage resp = await client.SendAsync(MPNSToastNotificationMessage());
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine("Send toast response status code: {0}", resp.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region OAuth Authentication
        //Reference: http://msdn.microsoft.com/en-us/library/windows/apps/hh465407.aspx
        //Reference: http://msdn.microsoft.com/en-us/library/windows/apps/hh465435.aspx
        private async void Authenticate_Action()
        {
            //Get secret and sid from submitted app on developers account
            //Ctf alpha app secrets:
            //string _secret = "SnGjuzqmtufDvHHUCKfu/ky7LEo4KJ02";
            //string _sid = "ms-app://s-1-15-2-3730157216-3214134527-913033742-2737464962-3370646195-1666773266-804720855";

            //New app secrets:
            string secret = "1U3KbCyBxuy+NVtfFfvP8eZZiRbJn+gj";
            string sid = "ms-app://s-1-15-2-2618110313-2335821257-1330249440-2686717437-3962867114-1572315970-1553553458";

            Token = await GetAccessToken(secret, sid);
            Debug.WriteLine("type:{0}, expires:{1}, access:{2}", Token.TokenType, Token.ExpiresIn, Token.AccessToken);
        }

        private async Task<OAuthToken> GetAccessToken(string secret, string sid)
        {

            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/accesstoken.srf");
            Dictionary<string, string> dict = new Dictionary<string, string> {
                                                { "grant_type", "client_credentials" },
                                                { "client_id", sid },
                                                { "client_secret", secret },
                                                { "scope", "notify.windows.com"} };

            message.Content = new FormUrlEncodedContent(dict);
            HttpResponseMessage resp = await client.SendAsync(message);
            string content = await resp.Content.ReadAsStringAsync();
            return GetOAuthTokenFromJson(content);
        }

        [DataContract]
        public class OAuthToken
        {
            [DataMember(Name = "access_token")]
            public string AccessToken { get; set; }
            [DataMember(Name = "token_type")]
            public string TokenType { get; set; }

            [DataMember(Name = "expires_in")]
            public int ExpiresIn { get; set; }
        }
        #endregion

        #region Helpers and others
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Authenticate_Action();
        }
        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        private OAuthToken token;
        public OAuthToken Token
        {
            get
            {
                if(token == null) return new OAuthToken();
                return token;
            }

            set
            {
                if(token != value)
                {
                    token = value;
                }
            }
        }
        
        private OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var ser = new DataContractJsonSerializer(typeof(OAuthToken));
                var oAuthToken = (OAuthToken)ser.ReadObject(ms);
                return oAuthToken;
            }
        }

        private string getJsonContent()
        {
            return "{&quot;type&quot;,:&quot;toast&quot;,:&quot;param1&quot;,[{2.1}]:&quot;12345&quot;:&quot;param2&quot;:&quot;67890&quot;" + " &quot;message&quot;:&quot;" + toastBox.Text + "&quot;}";
        }

        private StreamContent ContentStreamFromString(string contentString)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(contentString);
            writer.Flush();
            stream.Position = 0;

            return new StreamContent(stream);
        }
        #endregion
    }
}
