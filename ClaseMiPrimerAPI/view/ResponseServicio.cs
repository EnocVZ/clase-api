using ConcesionariaBarrios.Modelos;

namespace ClaseMiPrimerAPI.view;

public class ResponseServicio : Response
{
    public int IdServicio { get; set; }
    public List<Servicio> ListaServicio { get; set; }
    public Servicio Servicio { get; set; }
    public Servicio ServicioEncontrado { get; set; }
        
}
