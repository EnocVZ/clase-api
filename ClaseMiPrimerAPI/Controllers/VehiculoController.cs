using Azure;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly BaseDatosContext _context;
        ResponseVehiculo _response = new ResponseVehiculo();

        public VehiculoController(BaseDatosContext vehiculoContext)
        {
            this._context = vehiculoContext;
        }


        [HttpGet]
        [Route("listaVehiculos")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> listaVehiculos()
        {
            var vehiculos = await _context.Vehiculo.ToListAsync();
            return Ok(vehiculos);
        }

        [HttpPost]
        [Route("crearVehiculo")]
        public async Task<ActionResult<ResponseVehiculo>> crearVehiculo(RequestVehiculo vehiculo)
        {
            try
            {
                Vehiculo guardarVehiculo = new Vehiculo 
                { 
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo, 
                    Color = vehiculo.Color,
                    Anio = vehiculo.Anio
                }; 
                await _context.Vehiculo.AddAsync(guardarVehiculo);
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Se agrego correctamente. ";
                _response.error = false;

                return Ok(_response);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarVehiculo")]
        public async Task<IActionResult> buscarVehiculo(int id)
        {
            Vehiculo buscarVehiculo = await _context.Vehiculo.FindAsync(id);

            if (buscarVehiculo == null)
            {
                _response.error = true;
                _response.message = "Vehiculo no encontrado. ";
                _response.code = 500;
                return NotFound(_response);
            }
            _response.error = false;
            _response.message = "Vehiculo encontrado.";
            _response.vehiculoEncontrado = buscarVehiculo;
            return Ok(_response);
        }

        [HttpPut]
        [Route("actualizarVehiculo")]
        public async Task<IActionResult> actualizarVehiculo(int id, RequestVehiculo vehiculo)
        {
            var vehiculoExiste = await _context.Vehiculo.FindAsync(id);

            if (vehiculoExiste == null)
            {
                _response.error = true;
                _response.message = "Vehiculo no encontrado.";
                _response.code = 500;
                return NotFound(_response);
            }

            vehiculoExiste.Marca = vehiculo.Marca;
            vehiculoExiste.Modelo = vehiculo.Modelo;
            vehiculoExiste.Color = vehiculo.Color;
            vehiculoExiste.Anio = vehiculo.Anio;
            await _context.SaveChangesAsync();

            _response.error = false;
            _response.vehiculoEncontrado = vehiculoExiste;
            _response.message = "Vehiculo actualizado";
            _response.code = 200;

            return Ok(_response);

        }

        [HttpDelete]
        [Route("eliminarVehiculo")]
        public async Task<IActionResult> eliminarVehiculo(int id)
        {
            var vehiculoEliminar = await _context.Vehiculo.FindAsync(id);

            if (vehiculoEliminar == null)
            {
                _response.error = true;
                _response.message = "Vehiculo con id: " + id + " no encontrado";
                _response.code = 500;
                return Ok(_response);
            }

            _context.Vehiculo.Remove(vehiculoEliminar);
            await _context.SaveChangesAsync(); 

            _response.error = false;
            _response.message = "Vehiculo eliminado. ";
            _response.code = 200;
            _response.vehiculoEncontrado = vehiculoEliminar; 
            return Ok(_response);
        }
    }
}
