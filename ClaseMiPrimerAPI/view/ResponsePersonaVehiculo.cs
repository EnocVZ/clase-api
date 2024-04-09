using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePersonaVehiculo : Response
    {
        public int IdPersonaVehiculo { get; set; }
        public PersonaVehiculo RelacionPersonaVehiculo { get; set; } 
        public List<PersonaVehiculo> personasVehiculos { get; set; }
    }
}
