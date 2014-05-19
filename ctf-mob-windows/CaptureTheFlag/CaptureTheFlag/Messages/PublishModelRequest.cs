namespace CaptureTheFlag.Messages
{
    using System;

    public sealed class PublishModelRequest<T> : PublishModelBase<T>
    {
        public PublishModelRequest(object sender, Action<T> action)
        {
            SenderId = sender.GetHashCode();
            Action = action;
        }
    }
}
