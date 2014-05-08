using Caliburn.Micro;
using CaptureTheFlag.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public interface ICommunicationService
    {
        //TODO: make ICommunicationService more generic
        RestRequestAsyncHandle GetAllGames(string token, Action<BindableCollection<GameD>> successCallback, Action<ServerErrorMessage> errorCallback);
        //RestRequestAsyncHandle GetAllUsers(string token, Action<BindableCollection<Game>> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle GetAllMaps(string token, Action<BindableCollection<GameMap>> successCallback, Action<ServerErrorMessage> errorCallback);

        #region Game (un)subscription 
        RestRequestAsyncHandle AddPlayerToGame(GameD game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle RemovePlayerFromGame(GameD game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback);
        #endregion

        RestRequestAsyncHandle RegisterPosition(GameD game, GeoCoordinate coordinate, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle SelectCharacter(Character character, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback);

        //Authorization requests
        RestRequestAsyncHandle RegisterUser(User user, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle LoginUser(User user, Action<Authenticator> successCallback, Action<ServerErrorMessage> errorCallback);

        //Game requests
        RestRequestAsyncHandle CreateGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameFields(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGame(GameD game, string token, Action<GameD> successCallback, Action<ServerErrorMessage> errorCallback);
        
        //Map requests
        RestRequestAsyncHandle CreateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameMapFields(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);

        //Item requests
        RestRequestAsyncHandle CreateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameItemFields(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);

        //User requests
        RestRequestAsyncHandle CreateUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateUserFields(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteUser(UserD user, string token, Action<UserD> successCallback, Action<ServerErrorMessage> errorCallback);

        //Character requests
        RestRequestAsyncHandle ReadCharacter(Character character, string token, Action<Character> successCallback, Action<ServerErrorMessage> errorCallback);
    }
}
