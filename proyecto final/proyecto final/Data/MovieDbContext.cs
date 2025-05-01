using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class MovieDbContext : DbContext
    {
        private static readonly string connectionString = "Server=localhost;Database=PeliculasDB;User Id=sa;Password=1234;TrustServerCertificate=true;";

        // Constructor vacío para que Entity Framework Core pueda usar OnConfiguring
        public MovieDbContext() { }

        public DbSet<Movie> Movies { get; set; }

        // Configurar la cadena de conexión directamente en OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);  // Asegúrate de que se está usando la cadena de conexión aquí
            }
        }

        // Configuración adicional del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .Property(m => m.BoxOffice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
