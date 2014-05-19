namespace CaptureTheFlag.Messages
{
    public class AppActionMessage
    {
        public enum ACTION
        {
            CREATE = 0,
            READ = 1,
            UPDATE = 2,
            DELETE = 3,
            NAVIGATE = 4
        }

        private ACTION action;
        public ACTION Action
        {
            get { return action; }
            set
            {
                if (action != value)
                {
                    action = value;
                }
            }
        }

        private object message;
        public object Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                }
            }
        }
    }
}
