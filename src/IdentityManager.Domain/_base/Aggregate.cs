namespace IdentityManager.Domain.Base
{
    public abstract class Aggregate : Entity
    {
        public IReadOnlyCollection<DomainEvent> DomainEvents => _events;

        public void ClearEvents()
        {
            _events.Clear();
        }

        protected void AddEvent(DomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        private readonly HashSet<DomainEvent> _events = new();
    }
}
