using Microsoft.EntityFrameworkCore;

namespace RestFullWebApi.Abstractions.IRepositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }

        IQueryable<T> GetAll();    

        Task<T> GetByIdAsync(int id);

        Task<bool> AddAsync(T data);

        bool Remove(T data);

        Task<bool> RemoveById(int id);

        bool Update(T data);

    }
}
