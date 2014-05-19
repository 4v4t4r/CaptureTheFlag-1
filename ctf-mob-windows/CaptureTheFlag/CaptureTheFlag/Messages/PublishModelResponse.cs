namespace CaptureTheFlag.Messages
{
    using System;

    public sealed class PublishModelResponse<T> : PublishModelBase<T>
    {
        public PublishModelResponse(PublishModelRequest<T> request, T model)
        {
            SenderId = request.SenderId;
            Action = request.Action;
            Model = model;
        }

        public T Model { get; private set; }
    }
}
