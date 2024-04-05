using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class GetVehiculo : Response 
    {
        public Vehiculo vehiculoEncontrado { get; set; }
        public List<Vehiculo>? ListaVehiculo { get; set; }
    }
}
