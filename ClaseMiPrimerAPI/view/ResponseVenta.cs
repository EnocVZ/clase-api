namespace ClaseMiPrimerAPI.view;
using ConcesionariaBarrios.Modelos;

public class ResponseVenta : Response
{
    public List<Venta> ListaVenta { get; set; }
    public int IdVenta { get; set; }
    public Venta Venta { get; set; }
    public Venta VentaEncontrada { get; set; }
}
