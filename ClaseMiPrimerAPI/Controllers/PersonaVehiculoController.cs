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
        private readonly PersonaVehiculoContext _context;
        ResponsePersonaVehiculo responsePersonaVehiculo = new ResponsePersonaVehiculo();

        public PersonaVehiculoController(PersonaVehiculoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listaRelacionesPersonasVehiculos")]
        public async Task<ActionResult<IEnumerable<PersonaVehiculo>>> listaRelacionesPersonasVehiculos()
        {
            var PersonaVehiculo = await _context.PersonaVehiculo.ToListAsync(); 
            return Ok(PersonaVehiculo);
        }
    }
}
