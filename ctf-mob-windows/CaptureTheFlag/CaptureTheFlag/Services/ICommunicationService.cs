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
        //TODO: new methods for check
        Task<IRestResponse> CreateGameAsync(string token, Game game);
        Task<IRestResponse> CreateItemAsync(string token, Item item);

        Task<IRestResponse> PatchGameAsync(string token, Game game);

        //TODO: make ICommunicationService more generic
        RestRequestAsyncHandle GetAllGames(string token, Action<BindableCollection<Game>> successCallback, Action<ServerErrorMessage> errorCallback);
        //RestRequestAsyncHandle GetAllUsers(string token, Action<BindableCollection<Game>> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle GetAllMaps(string token, Action<BindableCollection<GameMap>> successCallback, Action<ServerErrorMessage> errorCallback);

        #region Game (un)subscription 
        RestRequestAsyncHandle AddPlayerToGame(Game game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle RemovePlayerFromGame(Game game, string token, Action<HttpResponse> successCallback, Action<ServerErrorMessage> errorCallback);
        #endregion

        RestRequestAsyncHandle RegisterPosition(Game game, GeoCoordinate coordinate, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle SelectCharacter(Character character, string token, Action<object> successCallback, Action<ServerErrorMessage> errorCallback);

        //Authorization requests
        RestRequestAsyncHandle RegisterUser(User user, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle LoginUser(User user, Action<Authenticator> successCallback, Action<ServerErrorMessage> errorCallback);

        //Game requests
        RestRequestAsyncHandle CreateGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameFields(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGame(Game game, string token, Action<Game> successCallback, Action<ServerErrorMessage> errorCallback);
        
        //Map requests
        RestRequestAsyncHandle CreateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameMapFields(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGameMap(GameMap gameMap, string token, Action<GameMap> successCallback, Action<ServerErrorMessage> errorCallback);

        //Game Item requests
        RestRequestAsyncHandle CreateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateGameItemFields(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteGameItem(GameItem gameItem, string token, Action<GameItem> successCallback, Action<ServerErrorMessage> errorCallback);

        //Item requests
        RestRequestAsyncHandle CreateItem(Item item, string token, Action<Item> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadItem(Item item, string token, Action<Item> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateItem(Item item, string token, Action<Item> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateItemFields(Item item, string token, Action<Item> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteItem(Item item, string token, Action<Item> successCallback, Action<ServerErrorMessage> errorCallback);

        //User requests
        RestRequestAsyncHandle CreateUser(User user, string token, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle ReadUser(User user, string token, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateUser(User user, string token, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle UpdateUserFields(User user, string token, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);
        RestRequestAsyncHandle DeleteUser(User user, string token, Action<User> successCallback, Action<ServerErrorMessage> errorCallback);

        //Character requests
        RestRequestAsyncHandle ReadCharacter(Character character, string token, Action<Character> successCallback, Action<ServerErrorMessage> errorCallback);
    }
}
