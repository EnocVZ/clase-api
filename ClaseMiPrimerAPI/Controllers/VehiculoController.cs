using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using ClaseMiPrimerAPI;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;
        private readonly PersonaContext context;
        public VehiculoController(ILogger<VehiculoController> palogger, PersonaContext personaContext)
        {
            this.logger = palogger;
            context = personaContext;
        }
        [HttpPost]
        [Route("guardarEnDB")]

        public async Task<ActionResult<Vehiculo>> AgregarVehiculo(Vehiculo nuevoVehiculo)
        {
            try
            {
                var result = await context.Vehiculo.AddAsync(nuevoVehiculo);

                await context.SaveChangesAsync();

                return Ok(nuevoVehiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("ObtenerVehiculoBD")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> ObtenerVehiculos()
        {
            try
            {
                var vehiculos = await context.Vehiculo.ToListAsync();

                return Ok(vehiculos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("ActualizarVehiculoBD")]
        public async Task<ActionResult<Vehiculo>> ActualizarVehiculo(int vehiculoId, Vehiculo vehiculoActualizado)
        {
            try
            {
                var vehiculoEncontrado = await context.Vehiculo.FindAsync(vehiculoId);

                if (vehiculoEncontrado != null)
                {
                    vehiculoEncontrado.Marca = vehiculoActualizado.Marca;
                    vehiculoEncontrado.Modelo = vehiculoActualizado.Modelo;
                    vehiculoEncontrado.Anio = vehiculoActualizado.Anio;

                    await context.SaveChangesAsync();

                    return Ok(vehiculoEncontrado);
                }
                else
                {
                    return NotFound($"No se encontró un vehículo con el ID {vehiculoId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("EliminarVehiculoBD")]
        public async Task<ActionResult<Vehiculo>> EliminarVehiculo(int vehiculoId)
        {
            try
            {
                var vehiculoAEliminar = await context.Vehiculo.FindAsync(vehiculoId);

                if (vehiculoAEliminar != null)
                {
                    context.Vehiculo.Remove(vehiculoAEliminar);

                    await context.SaveChangesAsync();

                    return Ok(vehiculoAEliminar);
                }
                else
                {
                    return NotFound($"No se encontró un vehículo con el ID {vehiculoId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

 