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
        //Response _responseConcesionario = new Response();
        ResponseConcesionario _response = new ResponseConcesionario();

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
                _response.error = true;
                _response.message = "Concesionario no encontrado";
                _response.code = 500;
            }
            _response.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _response.message = "Concesionario encontrado";
            _response.code = 200;
            _response.ConcesionarioEncontrado = buscarConcesionario;// utilizar ctrl + alt + pulsar para multicursor. 
            return Ok(_response);
        }

        [HttpPut]
        [Route("actualizarConcesionario")]
        public async Task<IActionResult> actualizarConcesionario(int id, RequestConcesionario concesionario)
        {
            var concesionarioExiste = await _context.Concesionario.FindAsync(id);
            if (concesionarioExiste == null)
            {
                _response.error = true;
                _response.message = "Mecanico no encontrado. ";// utilizar ctrl + alt + pulsar para multicursor. 
                _response.code = 500;
            }
            concesionarioExiste.Nombre = concesionario.Nombre;
            concesionarioExiste.Direccion = concesionario.Direccion;

            await _context.SaveChangesAsync();

            _response.code = 200;
            _response.message = "Concesionario actualizado";
            _response.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _response.ConcesionarioEncontrado= concesionarioExiste;

            return Ok(_response);
        }


        [HttpDelete]
        [Route("eliminarConcesionario")]
        public async Task<IActionResult> eliminarConcesionario(int id)
        {
            var concesionarioEliminado = await _context.Concesionario.FindAsync(id);
            if (concesionarioEliminado == null)
            {
                _response.error = true;
                _response.message = "Concesionario no encontrado";
                _response.code = 500;
            }
            _context.Concesionario.Remove(concesionarioEliminado);
            await _context.SaveChangesAsync();

            _response.error = false;
            _response.message = "Concesionario eliminado";
            _response.code = 200;
            _response.ConcesionarioEncontrado= concesionarioEliminado;
            return Ok(_response);  
        }


    }
}
