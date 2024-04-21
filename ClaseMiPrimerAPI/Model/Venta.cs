namespace ConcesionariaBarrios.Modelos
{
    public class Venta
    {
        public int? Id {  get; set; }
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConcesionario { get; set; }
        public int IdVendedor { get; set; }

        //DESCRIPCION DE LA VENTA 
        public string NombrePersona { get; set; }
        public string NombreVendedor { get; set; }
        public string Marca { get; set; }

        public string Fecha { get; set; }
        public float Total {  get; set; }
    }
}
