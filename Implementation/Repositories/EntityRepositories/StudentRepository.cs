using RestFullWebApi.Abstractions.IRepositories.IStudentRepositories;
using RestFullWebApi.Context;
using RestFullWebApi.Entity.ApplicationEntites;

namespace RestFullWebApi.Implementation.Repositories.EntityRepositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository 
    {
        public StudentRepository(AppDBContext context) : base(context)
        {
 
        }
    }
}
