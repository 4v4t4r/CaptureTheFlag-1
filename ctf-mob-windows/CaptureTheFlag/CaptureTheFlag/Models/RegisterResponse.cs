using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public class RegisterResponse
    {
        public string url { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string nick { get; set; }
        public List<string> characters { get; set; }
        public object device_type { get; set; }
        public object device_id { get; set; }
        public object lat { get; set; }
        public object lon { get; set; }
    }
}
