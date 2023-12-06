namespace NetChill.Application.Interfaces.Repositories
{
    //Defines a generic repository which contains generic CRUD methods
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIntIdAsync(int IntId);
        Task<T> GetByGuidIdAsync(Guid GuidId);
        Task<List<T>> GetAllAsync();
        Task<T> InsertAsync(T entity);
        Task UpdateAsyncWithIntId(T entity);
        Task UpdateAsyncWithGuidId(T entity);
        Task DeleteAsync(T entity);
    }
}
