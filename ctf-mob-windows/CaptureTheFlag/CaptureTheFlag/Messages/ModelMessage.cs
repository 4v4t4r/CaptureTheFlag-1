using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Messages
{
    public class ModelMessage
    {
        public enum STATUS
        {
            SHOULD_GET = 0,
            UPDATE = 1,
            IN_STORAGE = 2,
            DELETED = 3,
            UPDATED = 4
        }

        public BindableCollection<string> keys;


        private STATUS status;
        public STATUS Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                }
            }
        }
    }
}
