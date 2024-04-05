using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class PutVehiculo : Response
    {
        public int IdVehiculo { get; set; }
        public List<Vehiculo> ListaVehiculo { get; set; }
    }
}
