using RestFullWebApi.Abstractions.IRepositories.ISchoolRepositories;
using RestFullWebApi.Context;
using RestFullWebApi.Entity.ApplicationEntites;

namespace RestFullWebApi.Implementation.Repositories.EntityRepositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(AppDBContext context) : base(context)
        {

        }
    }
}
