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
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly PersonaContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, PersonaContext personaContext)
        {
            logger = paramLogger;
            context = personaContext;
        }

        [HttpGet]
        [Route("listaPersonaDB")]
        public async Task<ActionResult<List<PersonaVehiculo>>> listaPersonaDB()
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

        //Formulario para capturar el nombre de la persona con el vehiculo mas lujoso.

        [HttpPost]
        [Route("registrarPersonaVehiculo")]

        public async Task<ActionResult<Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
        {
            try
            {
                Persona persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                };
                Vehiculo vehiculo = new Vehiculo
                {
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
                return BadRequest(ex);
            }
        }
    }
}
