using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;


namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController: ControllerBase
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        // private readonly PersonaContext context;
        private readonly PersonaVehiculoController context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, PersonaVehiculoContext personaVehiculoContext)
        {
            logger = paramLogger;
            //context = personaContext;
           // context = personaVehiculoContext;

        }







    }
}
