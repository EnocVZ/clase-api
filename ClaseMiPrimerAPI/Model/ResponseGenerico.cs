namespace ClaseMiPrimerAPI.Model
{
    public class ResponseGenerico
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }

        public List<PersonaVehiculo>? listaPersonaVehiculo { get; set; }
    }
}
