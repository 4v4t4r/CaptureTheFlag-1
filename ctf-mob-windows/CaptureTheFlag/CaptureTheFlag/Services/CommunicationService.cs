using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;
using Windows.Phone.System.Analytics;
using CaptureTheFlag.Models;
using Microsoft.Phone.Net.NetworkInformation;
using Caliburn.Micro;
using System.Reflection;
using Newtonsoft.Json.Linq;
using MySerializerNamespace;
using System.Net;
using Newtonsoft.Json;

namespace CaptureTheFlag.Services
{
    public class CommunicationService : ICommunicationService
    {
        private RestClient client;
        private enum DEVICE_TYPE { WP = 1 };

        public CommunicationService()
        {
            //TODO: Generalize for string parameter
            client = new RestClient("http://78.133.154.39:8888");
        }

        public RestRequestAsyncHandle Login<T>(string username, string password, Action<IRestResponse<T>> callback) where T : new()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            RestRequest request = new RestRequest("/token/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            string stringJson = JsonConvert.SerializeObject(new { username = username, password = password, device_type = DEVICE_TYPE.WP, device_id = HostInformation.PublisherHostId });
            request.AddParameter("application/json", stringJson, ParameterType.RequestBody);

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLine("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription);
                Debug.WriteLine(response.Content);
                Debug.WriteLine("Register method response");

                IEventAggregator eventAggregator = IoC.GetAll<IEventAggregator>().FirstOrDefault<IEventAggregator>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    LoginResponse log = response.Data as LoginResponse;
                    eventAggregator.Publish(log);
                }
                else
                {
                    ServerErrorMessage sm = new ServerErrorMessage();
                    sm.Code = response.StatusCode;
                    sm.Message = response.StatusDescription;
                    eventAggregator.Publish(sm);
                }
            });
        }

        public RestRequestAsyncHandle Register<T>(string username, string password, string email, Action<IRestResponse<T>> callback) where T : new()
        {
            RestRequest request = new RestRequest("/api/registration/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            string stringJson = JsonConvert.SerializeObject(new { username = username, password = password, email = email });
            request.AddParameter("application/json", stringJson, ParameterType.RequestBody);
            
            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLine("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription);
                Debug.WriteLine(response.Content);
                Debug.WriteLine("Register method response");

                IEventAggregator eventAggregator = IoC.GetAll<IEventAggregator>().FirstOrDefault<IEventAggregator>();
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    RegisterResponse reg = response.Data as RegisterResponse;
                    eventAggregator.Publish(reg);
                }
                else
                {
                    ServerErrorMessage sm = new ServerErrorMessage();
                    sm.Code = response.StatusCode;
                    sm.Message = response.StatusDescription;
                    eventAggregator.Publish(sm);
                }
            });
        }

        public RestRequestAsyncHandle GetAllGames<T>(string token, Action<IRestResponse<T>> callback) where T : new()
        {
            RestRequest request = new RestRequest("/api/games/", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            return client.ExecuteAsync<T>(request, response =>
            {
                //Debug.WriteLine("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription);
                //Debug.WriteLine(response.Content);
                //Debug.WriteLine("Register method response");

                //IEventAggregator eventAggregator = IoC.GetAll<IEventAggregator>().FirstOrDefault<IEventAggregator>();
                //if (response.StatusCode == System.Net.HttpStatusCode.Created)
                //{
                //    RegisterResponse reg = response.Data as RegisterResponse;
                //    eventAggregator.Publish(reg);
                //}
                //else
                //{
                //    ServerErrorMessage sm = new ServerErrorMessage();
                //    sm.Code = response.StatusCode;
                //    sm.Message = response.StatusDescription;
                //    eventAggregator.Publish(sm);
                //}
            });
        }

        public RestRequestAsyncHandle CreateGame<T>(string token, Action<IRestResponse<T>> callback) where T : new()
        {
            return null;
        }
    }
}
