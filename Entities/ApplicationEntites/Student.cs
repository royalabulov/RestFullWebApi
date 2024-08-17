using RestFullWebApi.Entity.Comman;

namespace RestFullWebApi.Entity.ApplicationEntites
{
    public class Student : BaseEntity
    {
        public int Name { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
