using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.Controllers;
using APClaseMiPrimerAPII_3.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                    Modelo = request.Modelo
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
    }
}
