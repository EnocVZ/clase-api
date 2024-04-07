using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore; //interactuar con la base de datos 

namespace ClaseMiPrimerAPI.DbListContext
{
    public class PersonaContext : DbContext /*es una sesion de Base de datos, Interactuar con ella*/
    {
        public PersonaContext(DbContextOptions<PersonaContext> options) : base(options)
        { /*el parametro es instancia de DbContextOptions que tiene config especificas para la base de datos*/
        }

        public DbSet<Persona> Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
