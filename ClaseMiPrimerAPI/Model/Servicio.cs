﻿using Microsoft.AspNetCore.Routing.Constraints;

namespace ConcesionariaBarrios.Modelos
{
    public class Servicio
    {
        public int? IdServicio { get; set; }
        public string Nombre { get; set; }
        private string Descripcion {  get; set; }
        public float Precio { get; set; }
    }
}
