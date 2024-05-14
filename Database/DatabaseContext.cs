using Login_With_JWT_Authentication.Model.Departments;
using Login_With_JWT_Authentication.Model.LoginAndRegistration;
using Microsoft.EntityFrameworkCore;

namespace Login_With_JWT_Authentication.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; } 
        public DbSet<DepartmentModel> Departments { get; set; }
    }
}
