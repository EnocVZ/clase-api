namespace ClaseMiPrimerAPI.Model
{
    public class DatosVenta
    {
        public int? IdVenta { get; set; }
        //DESCRIPCION DE LA VENTA 
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string NombreVendedor { get; set; }
        public string ApellidoVendedor { get; set; }
        public string NombreConcesionario{ get; set; }
        public string DireccionConcesionario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Total { get; set; }
    }
}
