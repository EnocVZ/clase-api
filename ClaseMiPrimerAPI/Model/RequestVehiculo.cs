namespace ClaseMiPrimerAPI.Model
{
    public class RequestVehiculo
    {
        public int IdConcesionaria { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string? Color { get; set; }
        public string? Anio { get; set; }
    }
}
