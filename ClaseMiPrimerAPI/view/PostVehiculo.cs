using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class PostVehiculo : Response
    {
        public Vehiculo Vehiculo { get; set; }
        public List<Vehiculo>? ListaVehiculo { get; set; }
    }
}
