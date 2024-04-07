namespace ClaseMiPrimerAPI.Model
{
    public class ResponseVehiculo
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public Vehiculo VehiculoGuardado { get; set; }
    }
}
