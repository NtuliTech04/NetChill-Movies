using NetChill.Domain.Entities.Movie;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    //Perform automated tracking operations.
    public class TrackCreationProgressRepository: ITrackCreationProgressRepository
    {

        private readonly IGenericRepository<TrackCreationProgress> _repository;

        public TrackCreationProgressRepository(IGenericRepository<TrackCreationProgress> repository)
        {
            _repository = repository;
        }

        /** Custom methods defined within the respective repository as follows. **/ 


        //Initiates tracker for movie creation progress
        public async Task InitiateTracker(Guid Id)
        {
            try
            {
                var tracker = new TrackCreationProgress()
                {
                    MovieRef = Id,
                    Progress = 100,
                    Status = "Step 1: CreationInitialized"
                };
                await _repository.InsertAsync(tracker);

            }
             catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.Error520, ex);
            }
        }

        //Updates movie creation tracker
        public async Task UpdateTracker(Guid Id)
        {
            try
            {
                var movieTrcker = _repository.Entities.FirstOrDefault(x => x.MovieRef == Id);

                //Sets the Tracker Id to the global IntId
                //Sets the Tracker Id to the Multitype BaseId

                //movieTrcker.IntId = movieTrcker.Id; 
                movieTrcker.BaseId = movieTrcker.Id;

                if (movieTrcker.Progress == 100)
                {
                    movieTrcker.Progress = 200;
                    movieTrcker.Status = "Step 2: CreationInProgress";
                }
                else if (movieTrcker.Progress == 200)
                {
                    movieTrcker.Progress = 300;
                    movieTrcker.Status = "Step 3: CreationComplete";
                }

                await _repository.UpdateAsync(movieTrcker);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.Error520, ex);
            }
        }
    }
}
