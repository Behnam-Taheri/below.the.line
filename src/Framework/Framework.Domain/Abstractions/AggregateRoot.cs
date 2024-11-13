using Framework.Core.Abstractions;

namespace Framework.Domain.Abstractions
{
    public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {

        private readonly IList<dynamic> _domainEvents = [];
        public void QueueEvent<TEvent>(TEvent eventToPublish) where TEvent : IEvent
        {
            _domainEvents.Add(eventToPublish);
        }

        public IEnumerable<dynamic> GetAllQueuedEvents() => _domainEvents;

    }
}
