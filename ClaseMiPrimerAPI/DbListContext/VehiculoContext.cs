using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.DbListContext
{
    public class VehiculoContext : DbContext
    {
        public VehiculoContext(DbContextOptions<VehiculoContext> options) : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        //public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehiculo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
