using RestFullWebApi.Abstractions.IRepositories;
using RestFullWebApi.Abstractions.IRepositories.ISchoolRepositories;
using RestFullWebApi.Abstractions.Services;
using RestFullWebApi.Entity.Comman;

namespace RestFullWebApi.Abstractions
{
    public interface IUnitOfWork :IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        Task<int> Commit();
    }
}
