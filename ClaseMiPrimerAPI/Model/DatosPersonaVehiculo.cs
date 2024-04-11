namespace ClaseMiPrimerAPI.Model
{
    public class DatosPersonaVehiculo
    {
        //esta es la tabla que se reflejara con l ainformacion necesaria. 
        public int? IdPersonaVehiculo { get; set; }
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Uso { get; set; }
    }
}
