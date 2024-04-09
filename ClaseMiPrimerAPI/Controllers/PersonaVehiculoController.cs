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
        private readonly VehiculoContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, VehiculoContext vehiculoContext)
        {
            logger = paramLogger;
            context = vehiculoContext;
        }

        [HttpGet]
        [Route("listaPersonaVehiculo")]
        public async Task<ActionResult<ResponseVehiculo>> listaPersonaVehiculo()
        {
            try
            {
                ResponseGenerico response = new ResponseGenerico();

                // Obtener la lista de PersonaVehiculo desde la base de datos
                List<PersonaVehiculo> savedData = await context.PersonaVehiculo.ToListAsync();

                response.Code = 200;
                response.Message = "Se muestra";
                response.Error = false;
                response.listaPersonaVehiculo = savedData;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
