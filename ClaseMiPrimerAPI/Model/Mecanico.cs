﻿namespace ConcesionariaBarrios.Modelos
{
    public class Mecanico
    {
        public int? Id { get; set; }
        public int IdConcesionaria { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Salario { get; set; }

    }
}