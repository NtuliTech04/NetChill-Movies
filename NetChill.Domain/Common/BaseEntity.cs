using System.ComponentModel.DataAnnotations.Schema;

namespace NetChill.Domain.Common
{
    //Contains a collection of domain events and also some helper methods to add, remove and clear domain events from this collection.
    public abstract class BaseEntity
    {
        #region Multitype Base Entity Key

        public object BaseId { get; set; }

        #endregion

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
