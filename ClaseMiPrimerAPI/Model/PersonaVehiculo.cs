using System.ComponentModel.DataAnnotations.Schema;

namespace ClaseMiPrimerAPI.Model
{
    public class PersonaVehiculo 
    {
        public int Id { get; set; }
        [Column("IdPersona")]
        public int IdPersona{ get; set; }
        [Column("IdVehiculo")]
        public int IdVehiculo { get; set; }

        public Persona Persona { get; set; } // Propiedad de navegación a Persona
        public Vehiculo Vehiculo { get; set; }

    }
}
