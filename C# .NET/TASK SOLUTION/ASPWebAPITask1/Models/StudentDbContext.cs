using Microsoft.EntityFrameworkCore;
using ASPWebAPITask1.Models;

namespace ASPWebAPITask1.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}
