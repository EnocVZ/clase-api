using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePutVehiculo : Response
    {
        public int idVehiculo{ get; set; }
        public List<Vehiculo> listaVehiculo{ get; set; }
    }
}
