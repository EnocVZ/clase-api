﻿namespace ClaseMiPrimerAPI.Model
{
    public class PersonaVehiculo 
    {
        public int Id { get; set; }
        public int IdPersona{ get; set; }
        public int IdVehiculo { get; set; }

        public string Marca { get; set; }
        public string Nombre { get; set; }
        
    }
}
