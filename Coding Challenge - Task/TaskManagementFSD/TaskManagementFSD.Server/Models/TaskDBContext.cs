using TaskManagementFSD.Server.Enums;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementFSD.Server.Models
{
    public class TaskDBContext : DbContext
    {
        public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options) { }
        public virtual DbSet<TaskModel> Tasks { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (Role)Enum.Parse(typeof(Role), v));

                modelBuilder.Entity<TaskModel>()
                .Property(p => p.Priority)
                .HasConversion(
                    v => v.ToString(),
                    v => (Priority)Enum.Parse(typeof(Priority), v));

                modelBuilder.Entity<TaskModel>()
                    .Property(p => p.Status)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Status)Enum.Parse(typeof(Status), v));
        }
    }
}
