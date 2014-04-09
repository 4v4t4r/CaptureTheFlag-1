using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public class ServerErrorMessage
    {
        public System.Net.HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}
