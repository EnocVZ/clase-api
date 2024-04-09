using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonaVehiculoController : Controller
    {
        private readonly ILogger<VehiculoController> logger;
        private readonly BDContext context;
        public PersonaVehiculoController(ILogger<VehiculoController> palogger, BDContext BDContext)
        {
            this.logger = palogger;
            context = BDContext;
        }
        [HttpPost]
        [Route("AgregarPersona")]
        public async Task<ActionResult<Persona>> AgregarPersona(Persona nuevaPersona)
        {
            try
            {
                var result = await context.Persona.AddAsync(nuevaPersona);
                await context.SaveChangesAsync();
                return Ok(nuevaPersona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("ObtenerPersonas")]
        public async Task<ActionResult<IEnumerable<Persona>>> ObtenerPersonas()
        {
            try
            {
                var personas = await context.Persona.ToListAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ActualizarPersona")]
        public async Task<ActionResult<Persona>> ActualizarPersona(int personaId, Persona personaActualizada)
        {
            try
            {
                var personaEncontrada = await context.Persona.FindAsync(personaId);

                if (personaEncontrada != null)
                {
                    personaEncontrada.Nombre = personaActualizada.Nombre;
                    personaEncontrada.Apellido = personaActualizada.Apellido;
                    await context.SaveChangesAsync();
                    return Ok(personaEncontrada);
                }
                else
                {
                    return NotFound($"No se encontró una persona con el ID {personaId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("EliminarPersona")]
        public async Task<ActionResult<Persona>> EliminarPersona(int personaId)
        {
            try
            {
                var personaAEliminar = await context.Persona.FindAsync(personaId);

                if (personaAEliminar != null)
                {
                    context.Persona.Remove(personaAEliminar);
                    await context.SaveChangesAsync();
                    return Ok(personaAEliminar);
                }
                else
                {
                    return NotFound($"No se encontró una persona con el ID {personaId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AgregarVehiculo")]
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
        [Route("ObtenerVehiculos")]
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
        [Route("ActualizarVehiculo")]
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
        [Route("EliminarVehiculo")]
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
