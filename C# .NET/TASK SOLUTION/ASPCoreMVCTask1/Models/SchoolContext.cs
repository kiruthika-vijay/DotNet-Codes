using Microsoft.EntityFrameworkCore;

namespace ASPCoreMVCTask1.Models
{
    public class SchoolContext : DbContext
    {
        IConfiguration appconfig;
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Fee> Fees { get; set; }

        public SchoolContext(IConfiguration configuration)
        {
            appconfig = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(appconfig.GetConnectionString("SchoolConStr"));
            }
        }
    }
}
