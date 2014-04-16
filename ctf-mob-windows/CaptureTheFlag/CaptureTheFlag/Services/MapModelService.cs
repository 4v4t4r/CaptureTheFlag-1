using CaptureTheFlag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public class MapModelService : IMapModelService
    {
        private readonly ICommunicationService communicationService;

        public MapModelService(ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        public void Create(string token)
        {

        }

        public void Read(string token)
        {

        }

        public void Update(string token)
        {

        }

        public void Delete(string token)
        {

        }
    }
}
