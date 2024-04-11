
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly Contextt _context;
        ResponsePersonaVehiculo _response = new ResponsePersonaVehiculo();

        public PersonaVehiculoController(Contextt contextv)
        {
            _context = contextv;
        }
       
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<RequestPersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                //var savedData = await context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                for (var i = 0; i <= listaPersonaVehiculo.Count; i++)
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(persona => persona.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(car => car.Id == personaVehiculo.IdVehiculo).FirstOrDefault();


                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Modelo = vehiculo.Modelo
                        };

                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
                var PersonaAuto = await _context.PersonaVehiculo
                    .Join(_context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido
                    })
                    .ToListAsync();

                response.data = PersonaAuto;

                //nuevo
                var AutoPersona = await _context.PersonaVehiculo
                    .Join(_context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                    })
                    .ToListAsync();

                response.data = AutoPersona;
                //aqui acaba

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("listaRelacionesPersonasVehiculos")]
        public async Task<ActionResult<RequestPersonaVehiculo>> listaRelacionesPersonasVehiculos()
        {
            try
            {
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();
                for (var i = 0; i < listaPersonaVehiculo.Count; i++) //numero de ids cantidad de realciones
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(p => p.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(v => v.Id == personaVehiculo.IdVehiculo).FirstOrDefault();
                    if (persona != null && vehiculo != null && personaVehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Modelo = vehiculo.Modelo,
                           // Color = vehiculo.Color
                        };
                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
                _response.error = false;
                _response.code = 200;
                _response.data = listaDatosPersonaVehiculo;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                //Object reference not set to an instance of an object.

            }

        }




    }
}




