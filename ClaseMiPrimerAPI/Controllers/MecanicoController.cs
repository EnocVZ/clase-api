using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.view;
using ConcesionariaBarrios.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MecanicoController : ControllerBase
    {
        private readonly BaseDatosContext _context; 
        Response _response = new Response();
        ResponseMecanico  _responseMecanico = new ResponseMecanico();
        public MecanicoController(BaseDatosContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("listaMecanicos")]
        public async Task<ActionResult<IEnumerable<Mecanico>>> listaMecanicos()
        {
            try
            {
                var mecanicos = await _context.Mecanico.ToListAsync(); 
                return Ok(mecanicos); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregarMecanico")]
        public async Task<ActionResult<ResponseMecanico>> agregarMecanico(RequestMecanico mecanico)
        {
            try
            {
                Mecanico guardarMecanico = new Mecanico
                {
                    Nombre = mecanico.Nombre,
                    Apellido = mecanico.Apellido,
                    Salario = mecanico.Salario
                }; 
                await _context.Mecanico.AddAsync(guardarMecanico);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Mecanico agregado. ";
                _response.error = false;
                return Ok(_response); 
            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet]
        [Route("buscarMecanico")]
        public async Task<IActionResult> buscarMecanico(int id)
        {
            Mecanico buscarMecanico = await _context.Mecanico.FindAsync(id); 
            
            if(buscarMecanico == null)
            {
                _responseMecanico.error = true;
                _responseMecanico.message = "Mecanico no encontrado";
                _responseMecanico.code = 500; 
            }
            _responseMecanico.error = false;
            _responseMecanico.message = "Mecanico encontrado";
            _responseMecanico.mecanicoEncontrado = buscarMecanico;
            _responseMecanico.code = 200; 
            return Ok(_responseMecanico);
        }
        
        [HttpPut]
        [Route("actualizarMecanico")]
        public async Task<IActionResult> actualizarMecanico(int id, RequestMecanico mecanico)
        {
            var mecanicoExiste = await _context.Mecanico.FindAsync(id); 
            if(mecanicoExiste == null)
            {
                _responseMecanico.error = true;
                _responseMecanico.message = "Mecanico no encontrado. ";
                _responseMecanico.code = 500; 
            }
            mecanicoExiste.Nombre = mecanico.Nombre; 
            mecanicoExiste.Apellido = mecanico.Apellido;
            mecanicoExiste.Salario = mecanico.Salario;
            await _context.SaveChangesAsync(); 

            _responseMecanico.code = 200;
            _responseMecanico.message = "Mecanico actualizado";
            _responseMecanico.mecanicoEncontrado = mecanicoExiste;
            _responseMecanico.error = false; 
            
            return Ok(_responseMecanico);
        }

        [HttpDelete]
        [Route("eliminarMecanico")]
        public async Task<IActionResult> eliminarMecanico (int id)
        {
            var mecanicoEliminado = await _context.Mecanico.FindAsync(id); 
            if (mecanicoEliminado == null)
            {
                _responseMecanico.error= true;
                _responseMecanico.message = "Mecanino no encontrado";
                _responseMecanico.code = 500; 
            }
            _context.Mecanico.Remove(mecanicoEliminado);
            await _context.SaveChangesAsync();

            _responseMecanico.error = false;
            _responseMecanico.message = "Mecanico eliminado";
            _responseMecanico.code = 200;
            _responseMecanico.mecanicoEncontrado = mecanicoEliminado; 
            return Ok(_responseMecanico);
        }
    }
}
