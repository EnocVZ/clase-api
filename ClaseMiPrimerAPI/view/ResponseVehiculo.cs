namespace ClaseMiPrimerAPI      .Model
{
    public class ResponseVehiculo
    {
        public int code { get; set; }
        public string message { get; set; }

        public bool error { get; set; }

        public List<Vehiculo> listaVehiculo { get; set; }
    }
}
