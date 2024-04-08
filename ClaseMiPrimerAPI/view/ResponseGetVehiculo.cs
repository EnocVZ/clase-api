using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseGetVehiculo : Response
    {
        public List<Vehiculo>? listVehiculo { get; set; }
        public Vehiculo vehiculoEncontrado { get; set; }
    }
}
