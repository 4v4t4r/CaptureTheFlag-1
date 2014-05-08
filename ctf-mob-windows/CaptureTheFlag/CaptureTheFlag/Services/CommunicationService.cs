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
using System.Net;
using Newtonsoft.Json;
using System.Device.Location;

namespace CaptureTheFlag.Services
{
    public class CommunicationService : ICommunicationService
    {
        private RestClient client;

        public CommunicationService()
        {
            //TODO: Generalize for string parameter
            client = new RestClient("http://78.133.154.39:8888");
        }

        //TODO: reimplement if needed
        public RestRequestAsyncHandle GetAllUsers<T>(string token, Action<IRestResponse<T>> callback) where T : new()
        {
            RestRequest request = new RestRequest("/api/users/", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            return client.ExecuteAsync(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} _ Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);
            });
        }

        // --- new Interfaces: ---

        public RestRequestAsyncHandle LoginUser(User user, Action<Authenticator> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            RestRequest request = new RestRequest("/token/", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;

            user.DeviceType = User.DEVICE_TYPE.WP;
            user.DeviceId = HostInformation.PublisherHostId;

            string objectJsonString = JsonConvert.SerializeObject(user, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DebugLogger.WriteLineIf(objectJsonString != null, String.Format("Sending JSON: {0}", objectJsonString));
            request.AddParameter("application/json", objectJsonString, ParameterType.RequestBody);

            return client.ExecuteAsync<Authenticator>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<Authenticator>(response, errorCallback);
                }
            });
        }

        public RestRequestAsyncHandle RegisterUser(User user, Action<User> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            RestRequest request = new RestRequest("/api/registration/", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;

            string objectJsonString = JsonConvert.SerializeObject(user, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DebugLogger.WriteLineIf(objectJsonString != null, String.Format("Sending JSON: {0}", objectJsonString));
            request.AddParameter("application/json", objectJsonString, ParameterType.RequestBody);

            return client.ExecuteAsync<User>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<User>(response, errorCallback);
                }
            });
        }

        public RestRequestAsyncHandle RegisterPosition(GameD game, GeoCoordinate coordinate, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Put<object>(new Uri(game.url).PathAndQuery + "location/", token, new { lat = coordinate.Latitude, lon = coordinate.Longitude }, successCallback, errorCallback); 
        }

        public RestRequestAsyncHandle SelectCharacter(Character character, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Patch<object>(new Uri(character.url).PathAndQuery, token, new { is_active = true }, successCallback, errorCallback); 
        }


        #region Player requests
        public RestRequestAsyncHandle AddPlayerToGame(GameD game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Post<HttpResponse>(new Uri(game.url).PathAndQuery + "player/", token, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle RemovePlayerFromGame(GameD game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Delete<HttpResponse>(new Uri(game.url).PathAndQuery + "player/", token, successCallback, errorCallback);
        }
        #endregion

        #region Character requests
        public RestRequestAsyncHandle ReadCharacter(Character character, string token, Action<Character> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<Character>(new Uri(character.url).PathAndQuery, token, successCallback, errorCallback);
        }
        #endregion

        #region User requests
        public RestRequestAsyncHandle CreateUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Create<UserD>("/api/users/", token, user, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle ReadUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<UserD>(new Uri(user.url).PathAndQuery, token, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle UpdateUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Put<UserD>(new Uri(user.url).PathAndQuery, token, user, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle UpdateUserFields(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
#warning not implemented correctly, diff the model
            return Patch<UserD>(new Uri(user.url).PathAndQuery, token, user, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle DeleteUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Delete<UserD>(new Uri(user.url).PathAndQuery, token, successCallback, errorCallback);
        }
        #endregion

        #region Item requests
        public RestRequestAsyncHandle CreateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Create<GameItem>("/api/items/", token, gameItem, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle ReadGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<GameItem>(new Uri(gameItem.url).PathAndQuery, token, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle UpdateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Put<GameItem>(new Uri(gameItem.url).PathAndQuery, token, gameItem, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle UpdateGameItemFields(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback)
        {
#warning not implemented correctly, diff the model
            return Patch<GameItem>(new Uri(gameItem.url).PathAndQuery, token, gameItem, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle DeleteGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Delete<GameItem>(new Uri(gameItem.url).PathAndQuery, token, successCallback, errorCallback);
        }
        #endregion

        #region Map requests
        public RestRequestAsyncHandle GetAllMaps(string token, Action<BindableCollection<GameMap>> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<BindableCollection<GameMap>>("/api/maps/", token, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle CreateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Create<GameMap>("/api/maps/", token, gameMap, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle ReadGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<GameMap>(new Uri(gameMap.url).PathAndQuery, token, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle UpdateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Put<GameMap>(new Uri(gameMap.url).PathAndQuery, token, gameMap, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle UpdateGameMapFields(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback)
        {
#warning not implemented correctly, diff the model
            return Patch<GameMap>(new Uri(gameMap.url).PathAndQuery, token, gameMap, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle DeleteGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Delete<GameMap>(new Uri(gameMap.url).PathAndQuery, token, successCallback, errorCallback);
        }
        #endregion

        #region Game requests
        public RestRequestAsyncHandle GetAllGames(string token, Action<BindableCollection<GameD>> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<BindableCollection<GameD>>("/api/games/", token, successCallback, errorCallback);
        }
        public RestRequestAsyncHandle CreateGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Create<Game>("/api/games/", token, game, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle ReadGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Get<GameD>(new Uri(game.url).PathAndQuery, token, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle UpdateGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Put<GameD>(new Uri(game.url).PathAndQuery, token, game, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle UpdateGameFields(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
#warning not implemented correctly, diff the model
            return Patch<GameD>(new Uri(game.url).PathAndQuery, token, game, successCallback, errorCallback);
        }

        public RestRequestAsyncHandle DeleteGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback)
        {
            return Delete<GameD>(new Uri(game.url).PathAndQuery, token, successCallback, errorCallback);
        }
        #endregion

        #region Generic requests
        private RestRequestAsyncHandle Get<T>(string url, string token, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<T>(response, errorCallback);
                }
            });
        }

        private RestRequestAsyncHandle Delete<T>(string url, string token, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.DELETE);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<T>(response, errorCallback);
                }
            });
        }

        private RestRequestAsyncHandle Create<T>(string url, string token, T model, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.POST);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            string objectJsonString = JsonConvert.SerializeObject(model, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DebugLogger.WriteLineIf(objectJsonString != null, String.Format("Sending JSON: {0}", objectJsonString));
            request.AddParameter("application/json", objectJsonString, ParameterType.RequestBody);

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<T>(response, errorCallback);
                }
            });
        }

        private RestRequestAsyncHandle Put<T>(string url, string token, T model, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            string objectJsonString = JsonConvert.SerializeObject(model, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DebugLogger.WriteLineIf(objectJsonString != null, String.Format("Sending JSON: {0}", objectJsonString));
            request.AddParameter("application/json", objectJsonString, ParameterType.RequestBody);

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseData = response.Data;
                    successCallback(responseData);
                }
                else
                {
                    ServerErrorAction<T>(response, errorCallback);
                }
            });
        }

        private RestRequestAsyncHandle Post<T>(string url, string token, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.POST);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) successCallback(response.Data);
                else ServerErrorAction<T>(response, errorCallback);
            });
        }

        private RestRequestAsyncHandle Patch<T>(string url, string token, T model, Action<T> successCallback, Action<ServerErrorMessage> errorCallback) where T : new()
        {
            RestRequest request = new RestRequest(url, Method.PATCH);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", String.Format("Token {0}", token));

            string objectJsonString = JsonConvert.SerializeObject(model, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DebugLogger.WriteLineIf(objectJsonString != null, String.Format("Sending JSON: {0}", objectJsonString));
            request.AddParameter("application/json", objectJsonString, ParameterType.RequestBody);

            return client.ExecuteAsync<T>(request, response =>
            {
                Debug.WriteLineIf(response != null, String.Format("Status Code:{0} Status Description:{1}", response.StatusCode, response.StatusDescription));
                Debug.WriteLineIf(response != null, response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) successCallback(response.Data);
                else ServerErrorAction<T>(response, errorCallback);
            });
        }
        #endregion

        #region Helpers
        private void ServerErrorAction<T>(IRestResponse<T> response, Action<ServerErrorMessage> errorCallback)
        {
            ServerErrorMessage serverErrorMessage = new ServerErrorMessage();
            serverErrorMessage.Code = response.StatusCode;
            serverErrorMessage.Message = response.StatusDescription;
            errorCallback(serverErrorMessage);
        }
        #endregion
    }
}
