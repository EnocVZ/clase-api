using ClaseMiPrimerAPI.DbListContext;
using Microsoft.AspNetCore.Mvc;
using ClaseMiPrimerAPI.view;
using ConcesionariaBarrios.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        ResponseServicio _response = new ResponseServicio();

        public ServicioController(BaseDatosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/listaServicios")]
        public async Task<ActionResult<IEnumerable<Servicio>>> listaServicios()
        {
            try
            {
                var servicios = await _context.Servicio.ToListAsync();
                return Ok(servicios); 
            }
            catch(Exception ex)
            {
                //_response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregarServicio")]
        public async Task<ActionResult<ResponseServicio>> agregarServicio(RequestServicio servicio)
        {
            try
            {
                Servicio guardarServicio = new Servicio
                {
                    IdConcesionaria = servicio.IdConcesionaria,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    Precio = servicio.Precio
                }; 

                await _context.Servicio.AddAsync(guardarServicio);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Servicio agregado. ";
                _response.error = false;
                return Ok(_response);
            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet]
        [Route("buscarServicio")]
        public async Task<IActionResult> buscarServicio(int id)
        {
            Servicio buscarServicio = await _context.Servicio.FindAsync(id);

            if (buscarServicio == null)
            {
                _response.error = true;
                _response.message = "Servicio no encontrado";
                _response.code = 500;
                return NotFound(_response);

            }
            _response.error = false;// utilizar ctrl +   alt + pulsar para multicursor. 
            _response.message = "Servicio encontrado";
            _response.code = 200;
            _response.ServicioEncontrado = buscarServicio;// utilizar ctrl + alt + pulsar para multicursor. 
            return Ok(_response);
        }

        [HttpPut]
        [Route("actualizarServicio")]
        public async Task<IActionResult> actualizarServicio(int id, RequestServicio servicio)
        {
            var servicioExiste = await _context.Servicio.FindAsync(id);
            if (servicioExiste == null)
            {
                _response.error = true;
                _response.message = "Servicio no encontrado. ";// utilizar ctrl + alt + pulsar para multicursor. 
                _response.code = 500;
            }
            servicioExiste.IdConcesionaria = servicio.IdConcesionaria;
            servicioExiste.Nombre = servicio.Nombre;
            servicioExiste.Descripcion = servicio.Descripcion;
            servicioExiste.Precio = servicio.Precio; 

            await _context.SaveChangesAsync();

            _response.code = 200;
            _response.message = "Servicio actualizado";
            _response.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _response.ServicioEncontrado = servicioExiste;

            return Ok(_response);
        }


        [HttpDelete]
        [Route("eliminarServicio")]
        public async Task<IActionResult> eliminarServicio(int id)
        {
            var servicioEliminado = await _context.Servicio.FindAsync(id);
            if (servicioEliminado == null)
            {
                _response.error = true;
                _response.message = "Servicio no encontrado";
                _response.code = 500;
            }
            _context.Servicio.Remove(servicioEliminado);
            await _context.SaveChangesAsync();

            _response.error = false;
            _response.message = "Servicio eliminado";
            _response.code = 200;
            _response.ServicioEncontrado = servicioEliminado;
            return Ok(_response);
        }

    }
}
