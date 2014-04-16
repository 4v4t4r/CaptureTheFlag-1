using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public class UserModelService : IUserModelService
    {

        private readonly ICommunicationService communicationService;

        public UserModelService(ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        public void Read(string token)
        {



        }
    }
}
