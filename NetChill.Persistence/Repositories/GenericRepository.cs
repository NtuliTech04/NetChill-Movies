using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Common;
using NetChill.Persistence.Contexts;

namespace NetChill.Persistence.Repositories
{
    //Perform standard CRUD operations.
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly NetChillDbContext _dbContext;

        public GenericRepository(NetChillDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<T> Entities => _dbContext.Set<T>();




        //List All Method
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }


        //Create Method
        public async Task<T> InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }


        //ReadBy Entity Id Method
        public async Task<T> GetByIdAsync(object entityId)
        {
            return await _dbContext.Set<T>().FindAsync(entityId);
        }

        
        //Update Method
        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.BaseId);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        //Delete Method
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
