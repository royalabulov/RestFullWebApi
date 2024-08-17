using RestFullWebApi.Entity.Comman;

namespace RestFullWebApi.Entity.ApplicationEntites
{
    public class School : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
