using CaptureTheFlag.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public class GameModelService : IGameModelService
    {
        private readonly ICommunicationService communicationService;

        public GameModelService(ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        public void Create(string token, Game game)
        {
            game.name = "test";
            game.start_time = "2014-06-28T16:00:00Z";
            game.max_players = 10;
            game.type = (int)Game.GAME_TYPE.FRAGS;
            game.visibility_range = 100.0f;
            game.action_range = 5.0f;
            game.map = "http://78.133.154.39:8888/api/maps/2/";
            game.url = null;

            //communicationService.CreateGame<Game>(token, game, response => { });
        }

        public void Read()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {

        }
    }
}
