using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.view;
using ConcesionariaBarrios.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcesionarioController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        Response _response = new Response();
        ResponseConcesionario _responseConcesionario = new ResponseConcesionario();

        public ConcesionarioController(BaseDatosContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("listaConcesionarios")]
        public async Task<ActionResult<IEnumerable<Concesionario>>> listaConcesionarios()
        {
            try
            {
                var concesionarios = await _context.Concesionario.ToListAsync();
                return Ok(concesionarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregarConcesionario")]
        public async Task<ActionResult<ResponseConcesionario>> agregarConcesionario(RequestConcesionario concesionario)
        {
            try
            {
                Concesionario guardarConcesionario = new Concesionario
                {
                    Nombre = concesionario.Nombre,
                    Direccion = concesionario.Direccion,
                    CantidadVehiculos = concesionario.CantidadVehiculos
                };
                await _context.Concesionario.AddAsync(guardarConcesionario);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Concesionario agregado. ";
                _response.error = false;
                return Ok(_response);
            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet]
        [Route("buscarConcesionario")]
        public async Task<IActionResult> buscarConcesionario(int id)
        {
            Concesionario buscarConcesionario = await _context.Concesionario.FindAsync(id);

            if (buscarConcesionario == null)
            {
                _responseConcesionario.error = true;
                _responseConcesionario.message = "Concesionario no encontrado";
                _responseConcesionario.code = 500;
            }
            _responseConcesionario.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _responseConcesionario.message = "Concesionario encontrado";
            _responseConcesionario.code = 200;
            _responseConcesionario.ConcesionarioEncontrado = buscarConcesionario;// utilizar ctrl + alt + pulsar para multicursor. 
            return Ok(_responseConcesionario);
        }

        [HttpPut]
        [Route("actualizarConcesionario")]
        public async Task<IActionResult> actualizarConcesionario(int id, RequestConcesionario concesionario)
        {
            var concesionarioExiste = await _context.Concesionario.FindAsync(id);
            if (concesionarioExiste == null)
            {
                _responseConcesionario.error = true;
                _responseConcesionario.message = "Mecanico no encontrado. ";// utilizar ctrl + alt + pulsar para multicursor. 
                _responseConcesionario.code = 500;
            }
            concesionarioExiste.Nombre = concesionario.Nombre;
            concesionarioExiste.Direccion = concesionario.Direccion;

            await _context.SaveChangesAsync();

            _responseConcesionario.code = 200;
            _responseConcesionario.message = "Concesionario actualizado";
            _responseConcesionario.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _responseConcesionario.ConcesionarioEncontrado= concesionarioExiste;

            return Ok(_responseConcesionario);
        }


        [HttpDelete]
        [Route("eliminarConcesionario")]
        public async Task<IActionResult> eliminarConcesionario(int id)
        {
            var concesionarioEliminado = await _context.Concesionario.FindAsync(id);
            if (concesionarioEliminado == null)
            {
                _responseConcesionario.error = true;
                _responseConcesionario.message = "Concesionario no encontrado";
                _responseConcesionario.code = 500;
            }
            _context.Concesionario.Remove(concesionarioEliminado);
            await _context.SaveChangesAsync();

            _responseConcesionario.error = false;
            _responseConcesionario.message = "Concesionario eliminado";
            _responseConcesionario.code = 200;
            _responseConcesionario.ConcesionarioEncontrado= concesionarioEliminado;
            return Ok(_responseConcesionario);  
        }


    }
}
