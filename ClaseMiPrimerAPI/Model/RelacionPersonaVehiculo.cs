namespace ClaseMiPrimerAPI.Model
{
    public class RelacionPersonaVehiculo
    {
        public int IdPersona { get; set; }
        public int IdVehiculo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Uso { get; set; }
    }
}
