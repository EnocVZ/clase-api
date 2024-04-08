using ClaseMiPrimerAPI.Model;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseGetPersona : Response
    {
        public List<Persona>? listaPersona { get; set; }
        public Persona personaEncontrada { get; set; }
    }
}
