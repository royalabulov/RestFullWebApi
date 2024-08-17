using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestFullWebApi.Abstractions.IRepositories;
using RestFullWebApi.Context;
using RestFullWebApi.Entity.Comman;

namespace RestFullWebApi.Implementation.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDBContext context;

        public Repository(AppDBContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T data)
        {
            EntityEntry<T> entity = await Table.AddAsync(data);
          
            return entity.State == EntityState.Added;
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = Table.AsQueryable();
            return await query.FirstOrDefaultAsync(data => data.ID == id);
        }

        public bool Remove(T data)
        {
            EntityEntry<T> entity = Table.Remove(data);
            return entity.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveById(int id)
        {
            var data = await Table.FindAsync(id);
            return Remove(data);
        }

        public bool Update(T data)
        {
            EntityEntry entity = Table.Update(data);
            return entity.State == EntityState.Modified;
        }
    }
}
