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
        private readonly VehiculoContext vehiculoContext;
        ResponseVehiculo responseVehiculo = new ResponseVehiculo();

        public VehiculoController(VehiculoContext vehiculoContext)
        {
            this.vehiculoContext = vehiculoContext;
        }


        [HttpGet]
        [Route("listaVehiculos")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> listaVehiculos()
        {
            var vehiculos = await vehiculoContext.Vehiculo.ToListAsync();
            return Ok(vehiculos);
        }

        [HttpPost]
        [Route("crearVehiculo")]
        public async Task<ActionResult<ResponseVehiculo>> crearVehiculo(Vehiculo vehiculo)
        {
            try
            {
                await vehiculoContext.Vehiculo.AddAsync(vehiculo);
                await vehiculoContext.SaveChangesAsync();

                responseVehiculo.code = 200;
                responseVehiculo.message = "Se agrego correctamente. ";
                responseVehiculo.error = false;
                responseVehiculo.Vehiculo = vehiculo; //eliminar 

                return Ok(responseVehiculo);
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
            Vehiculo buscarVehiculo = await vehiculoContext.Vehiculo.FindAsync(id);

            if (buscarVehiculo == null)
            {
                responseVehiculo.error = true;
                responseVehiculo.message = "Vehiculo no encontrado. ";
                responseVehiculo.code = 500;
                return NotFound(responseVehiculo);
            }
            responseVehiculo.error = false;
            responseVehiculo.message = "Vehiculo encontrado.";
            responseVehiculo.vehiculoEncontrado = buscarVehiculo;
            return Ok(responseVehiculo);
        }

        [HttpPut]
        [Route("actualizarVehiculo")]
        public async Task<IActionResult> actualizarVehiculo(int id, RequestVehiculo vehiculo)
        {
            var vehiculoExiste = await vehiculoContext.Vehiculo.FindAsync(id);

            if (vehiculoExiste == null)
            {
                responseVehiculo.error = true;
                responseVehiculo.message = "Vehiculo no encontrado.";
                responseVehiculo.code = 500;
                return NotFound(responseVehiculo);
            }

            vehiculoExiste.Marca = vehiculo.Marca;
            vehiculoExiste.Modelo = vehiculo.Modelo;
            vehiculoExiste.Color = vehiculo.Color;
            vehiculoExiste.Anio = vehiculo.Anio;
            await vehiculoContext.SaveChangesAsync();

            responseVehiculo.error = false;
            responseVehiculo.vehiculoEncontrado = vehiculoExiste;
            responseVehiculo.message = "Vehiculo actualizado";
            responseVehiculo.code = 200;

            return Ok(responseVehiculo);

        }

        [HttpDelete]
        [Route("eliminarVehiculo")]
        public async Task<IActionResult> eliminarVehiculo(int id)
        {
            var vehiculoEliminar = await vehiculoContext.Vehiculo.FindAsync(id);

            if (vehiculoEliminar == null)
            {
                responseVehiculo.error = true;
                responseVehiculo.message = "Vehiculo con id: " + id + " no encontrado";
                responseVehiculo.code = 500;
                return Ok(responseVehiculo);
            }

            vehiculoContext.Vehiculo.Remove(vehiculoEliminar);
            await vehiculoContext.SaveChangesAsync(); 

            responseVehiculo.error = false;
            responseVehiculo.message = "Vehiculo eliminado. ";
            responseVehiculo.code = 200;
            responseVehiculo.vehiculoEncontrado = vehiculoEliminar; 
            return Ok(responseVehiculo);
        }
    }
}
