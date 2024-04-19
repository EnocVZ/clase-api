using ClaseMiPrimerAPI.DbListContext;
using Microsoft.AspNetCore.Mvc;
using ClaseMiPrimerAPI.view;
using ConcesionariaBarrios.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        ResponseVendedor _response = new ResponseVendedor();

        public VendedorController(BaseDatosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/listaVendedores")]
        public async Task<ActionResult<IEnumerable<Vendedor>>> listaServicios()
        {
            try
            {
                var vendedores = await _context.Vendedor.ToListAsync();
                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                //_response.message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregarVendedor")]
        public async Task<ActionResult<ResponseVendedor>> agregarVendedor(RequestVendedor vendedor)
        {
            try
            {
                Vendedor guardarVendedor = new Vendedor
                {
                    Nombre = vendedor.Nombre,
                    Apellido = vendedor.Apellido, 
                    Salario = vendedor.Salario
                };
                await _context.Vendedor.AddAsync(guardarVendedor);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Vendedor agregado. ";
                _response.error = false;
                return Ok(_response);
            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet]
        [Route("buscarVendedor")]
        public async Task<IActionResult> buscarVendedor(int id)
        {
            Vendedor buscarVendedor = await _context.Vendedor.FindAsync(id);

            if (buscarVendedor == null)
            {
                _response.error = true;
                _response.message = "Vendedor no encontrado";
                _response.code = 500;
                return NotFound(_response);

            }
            _response.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _response.message = "Vendedor encontrado";
            _response.code = 200;
            _response.VendedorEncontrado = buscarVendedor;// utilizar ctrl + alt + pulsar para multicursor. 
            return Ok(_response);
        }

        [HttpPut]
        [Route("actualizarVendedor")]
        public async Task<IActionResult> actualizarVendedor(int id, RequestVendedor vendedor)
        {
            var vendedorExiste = await _context.Vendedor.FindAsync(id);
            if (vendedorExiste == null)
            {
                _response.error = true;
                _response.message = "Vendedor no encontrado. ";// utilizar ctrl + alt + pulsar para multicursor. 
                _response.code = 500;
            }
            vendedorExiste.Nombre = vendedor.Nombre;
            vendedorExiste.Apellido = vendedor.Apellido;
            vendedorExiste.Salario = vendedor.Salario; 

            await _context.SaveChangesAsync();

            _response.code = 200;
            _response.message = "Vendedor actualizado";
            _response.error = false;// utilizar ctrl + alt + pulsar para multicursor. 
            _response.VendedorEncontrado = vendedorExiste;

            return Ok(_response);
        }


        [HttpDelete]
        [Route("eliminarVendedor")]
        public async Task<IActionResult> eliminarVendedor(int id)
        {
            var vendedorEliminado = await _context.Vendedor.FindAsync(id);
            if (vendedorEliminado == null)
            {
                _response.error = true;
                _response.message = "Servicio no encontrado";
                _response.code = 500;
            }
            _context.Vendedor.Remove(vendedorEliminado);
            await _context.SaveChangesAsync();

            _response.error = false;
            _response.message = "Servicio eliminado";
            _response.code = 200;
            _response.VendedorEncontrado = vendedorEliminado;
            return Ok(_response);
        }
    }
}
