using ASPCoreTask1MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreTask1MVC.Models
{
    public class SchoolContext : DbContext
    {
        IConfiguration appconfig;
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Fee> Fees { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options, IConfiguration configuration) : base(options)
        {
            appconfig = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(appconfig.GetConnectionString("SchoolConStr"));
            }
        }

        // Fluent API - higher precedence
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Student>()
        //        .Property(stu => stu.DOB)
        //        .HasColumnName("DateOfBirth")
        //        .HasColumnOrder(3)
        //        .HasColumnType("date");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Many to Many
        //    modelBuilder.Entity<Student>()
        //        .HasMany(s => s.Courses)
        //        .WithMany(c => c.Students);

        //    modelBuilder.Entity<Course>()
        //        .HasMany(c => c.Teachers)
        //        .WithMany(t => t.Courses);

        //    modelBuilder.Entity<Student>()
        //        .HasMany(s => s.Fees)
        //        .WithMany(f => f.Students);
        //}
    }
}
