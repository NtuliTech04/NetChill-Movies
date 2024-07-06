namespace NetChill.Application.Abstractions.Repositories.Movie
{
    //Implements the system automated tracking operations
    public interface ITrackCreationProgressRepository
    {
        Task InitiateTracker(Guid Id);

        Task UpdateTracker(Guid Id);
    }
}
