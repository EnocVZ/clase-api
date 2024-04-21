using Microsoft.AspNetCore.Routing.Constraints;

namespace ConcesionariaBarrios.Modelos
{
    public class Servicio
    {
        public int? Id { get; set; }
        public int IdConcesionaria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public int Precio { get; set; }
    }
}
