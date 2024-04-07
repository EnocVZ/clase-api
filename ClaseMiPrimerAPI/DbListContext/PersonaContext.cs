using ClaseMiPrimerAPI.Model; 
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ClaseMiPrimerAPI.DbListContext
{
    public class PersonaContext : DbContext
    {
        public PersonaContext(DbContextOptions<PersonaContext> options) : base(options)
        { 
        }
        
        public DbSet<Persona> Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
