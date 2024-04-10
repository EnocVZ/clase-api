namespace ClaseMiPrimerAPI.Model
{
    public class RequestPersonaVehiculo
    {
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Modelo { get; set; }

        public int Anio { get; set; }

        public string Marca { get; set; }
    }
}
