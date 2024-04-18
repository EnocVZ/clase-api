using ConcesionariaBarrios.Modelos;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseConcesionario : Response
    {
        public int IdVehiculo { get; set; }
        public Concesionario ConcesionarioEncontrado { get; set; }
        public Concesionario Concesionario { get; set; }
        public List<Concesionario> ListaConcesionarios { get; set; }
    }
}
