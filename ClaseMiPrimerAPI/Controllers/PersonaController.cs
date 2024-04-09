
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
        private readonly PersonaContext personaContext;
        public PersonaController(PersonaContext personaContext) {   
            this.personaContext = personaContext;
        }

        Response response = new Response();

        [HttpGet]
        [Route("listaPersonas")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> listaPersonas()
        {
            var personas = await personaContext.Persona.ToListAsync(); 
            return Ok(personas);    
        }

        [HttpPost]
        [Route("crearPersona")]
        public async Task<ActionResult<Response>> crearPersona(Persona persona)
        {
            try
            {   
                await personaContext.Persona.AddAsync(persona);
                await personaContext.SaveChangesAsync();

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
            Persona buscarPersona = await personaContext.Persona.FindAsync(id);

            ResponseGetPersona responseBuscar = new ResponseGetPersona();

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
            var personaExiste = await personaContext.Persona.FindAsync(id);
            ResponseGetPersona responseActualizar = new ResponseGetPersona();

            if (personaExiste == null)
            {
                responseActualizar.error = true;
                responseActualizar.message = "Persona no encontrada. ";
                responseActualizar.code = 500;
                return NotFound(responseActualizar);
            }

            personaExiste.Nombre = persona.Nombre;
            personaExiste.Apellido = persona.Apellido;
            await personaContext.SaveChangesAsync();

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
            var personaEliminada = await personaContext.Persona.FindAsync(id);
            ResponseGetPersona responseEliminar = new ResponseGetPersona();
            if (personaEliminada == null)
            {
                responseEliminar.error = true;
                responseEliminar.message = "Persona no encontrada. ";
                responseEliminar.code = 500;
                return NotFound(responseEliminar);
            }

            personaContext.Persona.Remove(personaEliminada);
            await personaContext.SaveChangesAsync();
            responseEliminar.error = false;
            responseEliminar.message = "Persona eliminada"; 
            responseEliminar.code = 200;
            responseEliminar.personaEncontrada = personaEliminada; 
            return Ok(responseEliminar);
        }
    }
}
