using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseGetVehiculo : Response
    {
        public List<Vehiculo>? listaVehiculo{ get; set; }
        public Vehiculo vehiculoEncontrado { get; set; }
    }
}
