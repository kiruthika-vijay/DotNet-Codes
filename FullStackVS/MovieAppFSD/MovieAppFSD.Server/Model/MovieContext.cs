using Microsoft.EntityFrameworkCore;

namespace MovieAppFSD.Server.Model
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
        public virtual DbSet<Movie> Movies { get; set;}
    }
}
