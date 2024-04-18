namespace ConcesionariaBarrios.Modelos
{
    public class Venta
    {
        public int? IdVenta {  get; set; }
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConcesionario { get; set; }
        public int IdVendedor { get; set; }
        public string Fecha { get; set; }
        public float Total {  get; set; }
    }
}
