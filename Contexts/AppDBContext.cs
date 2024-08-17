using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Entity.ApplicationEntites;

namespace RestFullWebApi.Context
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
    