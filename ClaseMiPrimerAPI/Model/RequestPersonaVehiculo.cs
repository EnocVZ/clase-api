namespace ClaseMiPrimerAPI.Model
{
    public class RequestPersonaVehiculo
    {
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }
}