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
        private static string uri = "";
        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        private static int messageId = 0;
        

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Authenticate_Action();
        }

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

                message.Headers.Add("X-WNS-Type", "wns/raw");
                message.Headers.Add("Authorization", String.Format("Bearer {0}", Token.AccessToken));
                message.Headers.Add("X-NotificationClass", "3");

                string contentString = "{ \"message\":\"" + toastBox.Text + "\"}";

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(contentString);
                writer.Flush();
                stream.Position = 0;

                message.Content = new StreamContent(stream);
                HttpResponseMessage resp = await client.SendAsync(message);
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine("Send toast response status code: {0}", resp.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void Authenticate_Action()
        {
            Token = await GetAccessToken("", "");
            Debug.WriteLine("type:{0}, expires:{1}, access:{2}", Token.TokenType, Token.ExpiresIn, Token.AccessToken);
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

        private OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var ser = new DataContractJsonSerializer(typeof(OAuthToken));
                var oAuthToken = (OAuthToken)ser.ReadObject(ms);
                return oAuthToken;
            }
        }

        protected async Task<OAuthToken> GetAccessToken(string secret, string sid)
        {
            //Get secret and sid from submitted app on developers account
            //Ctf alpha app secrets:
            //string _secret = "SnGjuzqmtufDvHHUCKfu/ky7LEo4KJ02";
            //string _sid = "ms-app://s-1-15-2-3730157216-3214134527-913033742-2737464962-3370646195-1666773266-804720855";

            //New app secrets:
            string _secret = "1U3KbCyBxuy+NVtfFfvP8eZZiRbJn+gj";
            string _sid = "ms-app://s-1-15-2-2618110313-2335821257-1330249440-2686717437-3962867114-1572315970-1553553458";
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/accesstoken.srf");
            Dictionary<string, string> dict = new Dictionary<string, string> {
                                                { "grant_type", "client_credentials" },
                                                { "client_id", _sid },
                                                { "client_secret", _secret },
                                                { "scope", "notify.windows.com"} };

            message.Content = new FormUrlEncodedContent(dict);
            HttpResponseMessage resp = await client.SendAsync(message);
            string content = await resp.Content.ReadAsStringAsync();
            return GetOAuthTokenFromJson(content);
        }
    }
}
