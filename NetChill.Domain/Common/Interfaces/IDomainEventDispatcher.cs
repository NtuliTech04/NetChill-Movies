
namespace NetChill.Domain.Common.Interfaces
{
    //Declares a method used to dispatch domain events throughout the application.
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
    }
}
