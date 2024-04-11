using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponsePersona : Response
    {
        public List<Persona>? listaPersona { get; set; }
        public Persona personaEncontrada { get; set; }
        public int idPersona { get; set; }
    }
}
