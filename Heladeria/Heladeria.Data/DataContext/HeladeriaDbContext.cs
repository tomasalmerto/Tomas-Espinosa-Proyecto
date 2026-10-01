using Microsoft.EntityFrameworkCore;
using Heladeria.Data.Entidades;

namespace Heladeria.Data.DataContext
{
    public class HeladeriaDbContext : DbContext
    {
        public HeladeriaDbContext(DbContextOptions<HeladeriaDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Sabor> Sabores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Cremas" },
                new Categoria { Id = 2, Nombre = "Frutales" },
                new Categoria { Id = 3, Nombre = "Chocolates" },
                new Categoria { Id = 4, Nombre = "Especialidades / Premium" }
            );
        }
    }
}

