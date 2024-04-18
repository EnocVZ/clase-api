using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
                                                                                    
namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        public PersonaController(BaseDatosContext personaContext) {   
            this._context = personaContext;
        }

        Response response = new Response();

        [HttpGet]
        [Route("listaPersonas")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> listaPersonas()
        {
            try
            {
                var personas = await _context.Persona.ToListAsync();
                return Ok(personas);
            }  
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("crearPersona")]
        public async Task<ActionResult<Response>> crearPersona(RequestPersona persona)
        {
            try
            {
                Persona guardarPersona = new Persona 
                { 
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido
                }; 
                await _context.Persona.AddAsync(guardarPersona);
                await _context.SaveChangesAsync();

                response.code = 200;    
                response.message = "Se guardo correctamente";
                response.error = false; 

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarPersona")]
        public async Task<IActionResult> buscarPersona(int id)
        {
            Persona buscarPersona = await _context.Persona.FindAsync(id);

            ResponsePersona responseBuscar = new ResponsePersona();

            if(buscarPersona == null)
            {
                responseBuscar.error = true;
                responseBuscar.message = "Persona no encontrada. ";
                responseBuscar.code = 500; 
                return NotFound(responseBuscar);
            }
            responseBuscar.error = false;
            responseBuscar.message = "Persona encontrada. ";
            responseBuscar.personaEncontrada = buscarPersona;
            responseBuscar.code = 200;
            return Ok(responseBuscar);
        }

        [HttpPut]
        [Route("actualizarPersona")]
        public async Task<IActionResult> actualizarPersona(int id, RequestPersona persona)
        {
            var personaExiste = await _context.Persona.FindAsync(id);
            ResponsePersona responseActualizar = new ResponsePersona();

            if (personaExiste == null)
            {
                responseActualizar.error = true;
                responseActualizar.message = "Persona no encontrada. ";
                responseActualizar.code = 500;
                return NotFound(responseActualizar);
            }

            personaExiste.Nombre = persona.Nombre;
            personaExiste.Apellido = persona.Apellido;
            await _context.SaveChangesAsync();

            responseActualizar.error = false;
            responseActualizar.personaEncontrada = personaExiste;
            responseActualizar.message = "Persona actualizada";
            responseActualizar.code = 200;

            return Ok(responseActualizar); 

        }

        [HttpDelete]
        [Route("eliminarPersona")]
        public async Task<IActionResult> eliminarPersona(int id)
        {
            var personaEliminada = await _context.Persona.FindAsync(id);
            ResponsePersona responseEliminar = new ResponsePersona();
            if (personaEliminada == null)
            {
                responseEliminar.error = true;
                responseEliminar.message = "Persona no encontrada. ";
                responseEliminar.code = 500;
                return NotFound(responseEliminar);
            }

            _context.Persona.Remove(personaEliminada);
            await _context.SaveChangesAsync();
            responseEliminar.error = false;
            responseEliminar.message = "Persona eliminada"; 
            responseEliminar.code = 200;
            responseEliminar.personaEncontrada = personaEliminada; 
            return Ok(responseEliminar);
        }
    }
}
