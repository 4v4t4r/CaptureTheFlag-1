using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public interface ICommunicationService
    {
        void Login();
        void Register(string username, string password, string email);
    }
}
