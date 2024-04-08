using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePutPersona : Response
    {
        public int idPersona { get; set; }
        public List<Persona> listaPersona { get; set; }
    }
}
