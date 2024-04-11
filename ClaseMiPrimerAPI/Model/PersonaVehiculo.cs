namespace ClaseMiPrimerAPI.Model
{
    public class PersonaVehiculo 
    {
        public int Id { get; set; }
        public int IdPersona{ get; set; }
        public int IdVehiculo { get; set; }

        public Persona Persona { get; set; } // Propiedad de navegación a Persona
        public Vehiculo Vehiculo { get; set; }

    }
}
