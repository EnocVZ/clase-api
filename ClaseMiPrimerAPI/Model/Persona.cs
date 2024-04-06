namespace ClaseMiPrimerAPI.Model
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Vehiculo Vehiculo { get; set; }
    }
}

