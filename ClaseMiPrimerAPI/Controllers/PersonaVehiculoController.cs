using API_3.DbListContext;
using API_3.Model;
using API_3.Controllers;
using API_3.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace API_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly PersonaContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, PersonaContext baseContext)
        {
            logger = paramLogger;
            context = baseContext;
        }
        [HttpGet]
        [Route("listaPersonaBD")]
        public async Task<ActionResult<List<PersonaVehiculo>>> listaPersonaBD()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var savedData = await context.PersonaVehiculo.ToListAsync();
                return Ok(savedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("eliminarPersonaDB")]
        public async Task<ActionResult<ResponseGetPersona>> eliminarPersonaBD(int id)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var personaEnDB = await context.Persona.FindAsync(id);
                context.Remove(personaEnDB);
                await context.SaveChangesAsync();


                response.code = 200;
                response.message = "se guardo";
                response.error = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPost]
        [Route("registrarPersonaVehiculo")]

        public async Task<ActionResult<Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
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
                };


                PersonaVehiculo personaVehiculo = new PersonaVehiculo();
                var savePersona = await context.Persona.AddAsync(persona);
                var saveVehiculo = await context.Vehiculo.AddAsync(vehiculo);
                await context.SaveChangesAsync();
                personaVehiculo.IdPersona = savePersona.Entity.Id;
                personaVehiculo.IdVehiculo = saveVehiculo.Entity.Id;
                await context.PersonaVehiculo.AddAsync(personaVehiculo);
                await context.SaveChangesAsync();

                Response response = new Response();
                return Ok(response);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await context.PersonaVehiculo.ToListAsync();
                var listaPersona = await context.Persona.ToListAsync();
                var listaVehiculo = await context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                var PersonaAuto = (from pv in context.PersonaVehiculo
                                   join p in context.Persona on pv.IdPersona equals p.Id
                                   join v in context.Vehiculo on pv.IdVehiculo equals v.Id
                                   select new { pv.Id, v, p }).ToList();



                var PersonaAuto2 = await context.PersonaVehiculo
                    .Join(context.Persona, personaVehiculo => personaVehiculo.IdPersona, persona => persona.Id, (personaVehiculo, persona) => new { personaVehiculo })
                    .Join(context.Vehiculo, joined => joined.personaVehiculo.IdVehiculo, vehiculo => vehiculo.Id, (joined, vehiculo) => new DatosPersonaVehiculo { 
                        Nombre = joined.persona.Nombre,
                        Marca = vehiculo.Marca
                    })
                    .ToListAsync();

                var listPrueba = await context.PersonaVehiculo.Select(pv => new
                {
                    persona = context.Persona.FindAsync(pv.IdPersona),
                    vehiculo = context.Vehiculo.FirstAsync(pv.IdVehiculo)
                }).ToListAsync();
                response.data = listaDatosPersonaVehiculo;

                var personaVehiculo = await context.PersonaVehiculo
                    .Join(context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido
                    }).ToListAsync();

                var vehiculoPersona = await context.PersonaVehiculo
                    .Join(context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                        Marca = vehiculo.Marca
                    }).ToListAsync();

                var listaPrueba = await context.PersonaVehiculo.Select(pv => new { }).ToListAsync();
                response.data = listaDatosPersonaVehiculo;

                return Ok(PersonaAuto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
