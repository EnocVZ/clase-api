using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePersonaVehiculo
    {
        public int IdPersonaVehiculo { get; set; }
        public PersonaVehiculo RPersonaVehiculo { get; set; } 
        public List<PersonaVehiculo> personasVehiculos { get; set; }
    }
}
