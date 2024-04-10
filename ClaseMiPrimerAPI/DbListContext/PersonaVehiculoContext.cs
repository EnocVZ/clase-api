using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;
using ClaseMiPrimerAPI;
using System.Data.Common;


namespace ClaseMiPrimerAPI.DbListContext
{
    public class PersonaVehiculoContext: DbContext
    {
        public PersonaVehiculoContext(DbContextOptions<PersonaVehiculoContext> options) : base(options)
        {
        }
        public DbSet<PersonaVehiculo> PersonaVehiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PersonaVehiculo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
