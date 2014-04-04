using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;

namespace CaptureTheFlag.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly RestClient client;

        public CommunicationService()
        {
            //TODO: Generalize for string parameter
            client = new RestClient("http://78.133.154.39:8888");
        }

        public void Login()
        {
            
        }

        public void Register(string username, string password, string email)
        {
            var request = new RestRequest("/api/registration/", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("email", email);

            client.ExecuteAsync(request, response =>
            {
                Debug.WriteLine("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription);
                Debug.WriteLine(response.Content);
            });
        }
    }
}
