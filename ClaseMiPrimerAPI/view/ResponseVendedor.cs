using ConcesionariaBarrios.Modelos;

namespace ClaseMiPrimerAPI.view
{
    public class ResponseVendedor : Response
    {
        public int IdVendedor {  get; set; }
        public List<Vendedor> ListaVendedor { get; set; }
        public Vendedor VendedorEncontrado { get; set; }

    }
}
