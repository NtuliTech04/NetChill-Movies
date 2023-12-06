namespace NetChill.Application.Interfaces.Repositories.Movie
{
    //Implements the system automated tracking operations
    public interface ITrackCreationProgressRepository
    {
        Task InitiateTracker(Guid Id);

        Task UpdateTracker(Guid Id);
    }
}
