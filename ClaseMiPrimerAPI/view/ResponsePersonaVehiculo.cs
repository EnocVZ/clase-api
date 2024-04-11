using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePersonaVehiculo : Response
    {

        public List<DatosPersonaVehiculo> data { get; set; }

        public int IdPersonaVehiculo { get; set; }
        public PersonaVehiculo RelacionPersonaVehiculo { get; set; } 
        public List<PersonaVehiculo> personasVehiculos { get; set; }
    }
}
