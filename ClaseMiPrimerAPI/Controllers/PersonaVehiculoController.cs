using Azure;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly DBContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, DBContext baseContext)
        {
            logger = paramLogger;
            context = baseContext;
        }

        //_________________________________________________________




        [HttpGet]
        [Route("listaPersonaBD")]
        //busqueda
        public async Task<ActionResult<List<PersonaVehiculo>>> listaPersonaBD()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var savedData = await context.PersonasVehiculo.ToListAsync();
                return Ok(savedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Formulario para capturar el nombre de la persona con el vehiculo mas lujoso

        [HttpPost]
        [Route("registrarPersonaVehiculo")]
        public async Task<ActionResult<ClaseMiPrimerAPI.view.Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
        {
            try
            {
                Persona persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido
                };

                Vehiculo vehiculo = new Vehiculo
                {
                    Marca = request.Marca,
                    Modelo = request.Modelo,
                    Anio = request.Anio,
                    Color = request.Color
                };

                await context.Persona.AddAsync(persona);
                await context.Vehiculo.AddAsync(vehiculo);

                await context.SaveChangesAsync(); // Guardar cambios una vez después de agregar las entidades

                PersonaVehiculo personaVehiculo = new PersonaVehiculo
                {
                    IdPersona = persona.Id,
                    IdVehiculo = vehiculo.Id
                };

                await context.PersonasVehiculo.AddAsync(personaVehiculo);
                await context.SaveChangesAsync();

                ClaseMiPrimerAPI.view.Response response = new ClaseMiPrimerAPI.view.Response
                {
                    code = 200,
                    message = "Persona y vehículo registrados correctamente",
                    error = false
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ClaseMiPrimerAPI.view.Response
                {
                    code = 500,
                    message = ex.Message,
                    error = true
                });
            }
        }


        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<DatosPersonaVehiculo>>> listaPersona()
        {
            try
            {
                var listaPersonaVehiculo = await context.PersonasVehiculo.ToArrayAsync();
                var listaPersona = await context.Persona.ToArrayAsync();
                var listaVehiculo = await context.Vehiculo.ToArrayAsync();

                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();
                foreach (var personaVehiculo in listaPersonaVehiculo)
                {
                    var persona = listaPersona.FirstOrDefault(persona => persona.Id == personaVehiculo.IdPersona);
                    var vehiculo = listaVehiculo.FirstOrDefault(car => car.Id == personaVehiculo.IdVehiculo);
                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Marca = vehiculo.Marca,
                            Modelo = vehiculo.Modelo
                        };
                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }

                return Ok(listaDatosPersonaVehiculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("listaPersonaVehiculo")]
        public async Task<ActionResult<List<DatosPersonaVehiculo>>> listaPersonaVehiculo()
        {
            try
            {
                var listaDatosPersonaVehiculo = await context.PersonasVehiculo
                    .Include(pv => pv.Persona)
                    .Include(pv => pv.Vehiculo)
                    .Select(pv => new DatosPersonaVehiculo
                    {
                        IdPersonaVehiculo = pv.Id,
                        Nombre = pv.Persona.Nombre,
                        Apellido = pv.Persona.Apellido,
                        Marca = pv.Vehiculo.Marca,
                        Modelo = pv.Vehiculo.Modelo
                    })
                    .ToListAsync();

                return Ok(listaDatosPersonaVehiculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }










    }
}

