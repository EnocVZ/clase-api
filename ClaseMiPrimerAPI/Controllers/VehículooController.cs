/*using ClaseMiPrimerAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Vehiculos
{
    [ApiController]
    [Route("[controller]")]
    public class VehiculooController : ControllerBase

    {
        static List<Vehiculoo> vehiculos = new List<Vehiculoo>();
        static List<Persona> personas = new List<Persona>();

        static void Main(string[] args)
        {
            Vehiculoo vehiculo = new Vehiculoo { Id = 1, Marca = "Toyota", Modelo = "Corolla" };
            CrearVehiculo(vehiculo);
            vehiculo.Marca = "Honda";
            ActualizarVehiculo(vehiculo);
            Vehiculoo vehiculoObtenido = ObtenerVehiculo(1);
            EliminarVehiculo(1);

            Persona persona = new Persona { Id = 1, Nombre = "Jose", Apellido = "Prado" ,Vehiculo = vehiculoObtenido };
            CrearPersona(persona);
        }

        static void CrearVehiculo(Vehiculoo vehiculo)
        {
            vehiculos.Add(vehiculo);
        }

        static void ActualizarVehiculo(Vehiculoo vehiculo)
        {
            for (int i = 0; i < vehiculos.Count; i++)
            {
                if (vehiculos[i].Id == vehiculo.Id)
                {
                    vehiculos[i] = vehiculo;
                    break;
                }
            }
        }

        static Vehiculoo ObtenerVehiculo(int id)
        {
            foreach (var vehiculo in vehiculos)
            {
                if (vehiculo.Id == id)
                {
                    return vehiculo;
                }
            }
            return null;
        }

        static void EliminarVehiculo(int id)
        {
            vehiculos.RemoveAll(v => v.Id == id);
        }

        static void CrearPersona(Persona persona)
        {
            personas.Add(persona);
        }

    }

}*/