    using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseVehiculo : Response
    {   
        public int IdVehivulo { get; set; }
        public Vehiculo vehiculoEncontrado { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public List<Vehiculo> ListaVehiculos { get; set; }
            
    }
}
