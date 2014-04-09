using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public interface ICommunicationService
    {
        //TODO: make ICommunicationService more generic
        RestRequestAsyncHandle Login<T>(string username, string password, Action<IRestResponse<T>> callback) where T : new();
        RestRequestAsyncHandle Register<T>(string username, string password, string email, Action<IRestResponse<T>> callback) where T : new();
        
        RestRequestAsyncHandle CreateGame<T>(string token, Action<IRestResponse<T>> callback) where T : new();
        RestRequestAsyncHandle GetAllGames<T>(string token, Action<IRestResponse<T>> callback) where T : new();
    }
}
