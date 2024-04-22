namespace ConcesionariaBarrios.Modelos
{
    public class RequestVenta
    {
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConcesionario { get; set; }
        public int IdVendedor { get; set; }
        public string Fecha { get; set; }
        public int Total { get; set; }
    }
}
