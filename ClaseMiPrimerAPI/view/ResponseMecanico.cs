using ConcesionariaBarrios.Modelos;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseMecanico : Response
    {
        public List<Mecanico> listaMecanico {  get; set; }
        public Mecanico mecanicoEncontrado { get; set; }
        public int idMecanico { get; set; }
    }
}
