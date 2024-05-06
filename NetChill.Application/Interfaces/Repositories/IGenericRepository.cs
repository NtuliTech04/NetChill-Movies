namespace NetChill.Application.Interfaces.Repositories
{
    //Defines a generic repository which contains generic CRUD methods
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(object EntityId);
        Task<List<T>> GetAllAsync();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


        //Task<T> GetByIntIdAsync(int IntId);
        //Task<T> GetByIntIdAsync(object IntId);
        //Task<T> GetByGuidIdAsync(Guid GuidId);
        //Task<T> GetByGuidIdAsync(object GuidId);

        //Task UpdateAsyncWithIntId(T entity);
        //Task UpdateAsyncWithGuidId(T entity);
    }
}
