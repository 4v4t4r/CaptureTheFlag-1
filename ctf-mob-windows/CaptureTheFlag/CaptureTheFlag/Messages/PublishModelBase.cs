namespace CaptureTheFlag.Messages
{
    using System;

    public abstract class PublishModelBase<T>
    {
        public int SenderId { get; protected set; } //TODO: When updated for WP 8.1 use ObjectIDGenerator for identification
        public Action<T> Action { get; protected set; }
    }
}
