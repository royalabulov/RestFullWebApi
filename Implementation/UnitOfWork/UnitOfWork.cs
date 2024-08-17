using RestFullWebApi.Abstractions;
using RestFullWebApi.Abstractions.IRepositories;
using RestFullWebApi.Abstractions.IRepositories.ISchoolRepositories;
using RestFullWebApi.Abstractions.Services;
using RestFullWebApi.Context;
using RestFullWebApi.Entity.Comman;
using RestFullWebApi.Implementation.Repositories;

namespace RestFullWebApi.Implementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;

        private Dictionary<Type, object> repositories;
        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
            repositories = new Dictionary<Type, object>();
        }




        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)repositories[typeof(TEntity)];
            }

            IRepository<TEntity> repository = new Repository<TEntity>(context);
            repositories.Add(typeof(TEntity), repository);
            return repository;

        }
    }
}

