namespace Framework.Core.Abstractions
{
    public interface IAggregateRoot
    {
        void QueueEvent<TEvent>(TEvent eventToPublish) where TEvent : IEvent;

        IEnumerable<dynamic> GetAllQueuedEvents();
    }
}
