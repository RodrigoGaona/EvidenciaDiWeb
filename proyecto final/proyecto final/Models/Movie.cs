using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models

{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int ReleaseYear { get; set; }
        public string? MainActors { get; set; }
        public decimal BoxOffice { get; set; }
    }

    // Esta es la configuración adicional que agregamos en la clase DbContext.
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Especificar precisión y escala para BoxOffice
            modelBuilder.Entity<Movie>()
                .Property(m => m.BoxOffice)
                .HasColumnType("decimal(18, 2)"); // Precisión 18, escala 2
        }
    }
}
