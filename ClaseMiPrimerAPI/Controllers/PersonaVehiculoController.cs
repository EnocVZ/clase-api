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
        ResponsePersonaVehiculo _response = new ResponsePersonaVehiculo();

        public PersonaVehiculoController(PersonaVehiculoContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        [Route("listaRelacionesPersonasVehiculos")]
        public async Task<ActionResult<IEnumerable<PersonaVehiculo>>> listaRelacionesPersonasVehiculos()
        {
            var PersonaVehiculo = await _context.PersonaVehiculo.ToListAsync(); 
            return Ok(PersonaVehiculo);
        }

        [HttpPost]
        [Route("agregarRelacionPersonaVehiculo")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> agregarRelacionPersonaVehiculo(PersonaVehiculo personaVehiculo)
        {
            try
            {
                await _context.PersonaVehiculo.AddAsync(personaVehiculo);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Relacion agregada";
                _response.error = false;
                _response.RelacionPersonaVehiculo = personaVehiculo;
                return Ok(_response); 
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarRelacionPersonaVehiculo")]
        public async Task<IActionResult> buscarRelacionPersonaVehiculo(int id)
        {
            PersonaVehiculo buscarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            
            if(buscarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada"; 
                _response.error = true;
                return Ok(_response); 
            }
            else
            {
                _response.code = 200;
                _response.message = "Relacion encontrada"; 
                _response.error = false;
                _response.RelacionPersonaVehiculo = buscarRelacion;
                return Ok(_response );
            }
        }

        [HttpPut]
        [Route("actualizarRelacionPersonaVehiculo")]
        public async Task<IActionResult> actualizarRelacionPersonaVehiculo (PersonaVehiculo personaVehiculo)
        {
            var buscarRelacion = await _context.PersonaVehiculo.FindAsync(personaVehiculo.Id); //consulta que debo agregar al metodo de agregar relacion 
            if(buscarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada";
                _response.error = true;
                return Ok(_response); 
            }
            else {
                buscarRelacion.IdPersona = personaVehiculo.IdPersona;
                buscarRelacion.IdVehiculo = personaVehiculo.IdVehiculo;
                buscarRelacion.Uso = personaVehiculo.Uso;
                await _context.SaveChangesAsync(); 

                _response.code = 200;
                _response.message = "Relacion actualizada. ";
                _response.error = false; 
                return Ok(_response);
            }
        }

        [HttpDelete]
        [Route("eliminarRelacionPersonaVehiculo")]
        public async Task<IActionResult> eliminarRelacionPersonaVehiculo (int id)
        {
            var eliminarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            if(eliminarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada"; 
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                /*            vehiculoContext.Vehiculo.Remove(vehiculoEliminar);
            await vehiculoContext.SaveChangesAsync(); */
                _context.PersonaVehiculo.Remove(eliminarRelacion);
                await _context.SaveChangesAsync();
                _response.code = 200;
                _response.message = "Relacion eliminada"; 
                _response.error = false;
                _response.RelacionPersonaVehiculo = eliminarRelacion; 
                return Ok(_response);
            }
        }


    }
}
