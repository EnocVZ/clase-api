namespace ConcesionariaBarrios.Modelos
{
    public class Venta
    {
        public int? Id {  get; set; }
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConcesionario { get; set; }
        public int IdVendedor { get; set; }

        //descripcion de la venta esta en DATOS venta

        public string Fecha { get; set; }
        public int Total {  get; set; }
    }
}
