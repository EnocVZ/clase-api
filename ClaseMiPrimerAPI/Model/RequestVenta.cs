namespace ConcesionariaBarrios.Modelos
{
    public class RequestVenta
    {
        private int IdPersona { get; set; }
        private int IdVehiculo { get; set; }
        private int IdConcesionario { get; set; }
        private int IdVendedor { get; set; }
        private string Fecha { get; set; }
        private float Total { get; set; }
    }
}
