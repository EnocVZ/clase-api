using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Controllers;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : Controller
    {
        private readonly ILogger<PersonaController> logger;
        private readonly DBcontext context;
        public PersonaVehiculoController(ILogger<PersonaController> paramLogger, PersonaContext personaContext)
        {
            this.logger = paramLogger;
            context = personaContext;
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
    }
}
