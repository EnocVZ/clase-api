using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaseMiPrimerAPI.Controllers
{
    public class VehiculoController : ControllerBase
    {
        private readonly VehiculoContext context;

        public VehiculoController(VehiculoContext dbContext)
        {
            context = dbContext;
        }
        [HttpPost]
        [Route("guardarVehiculo")]
        public async Task<ActionResult<ResponseVehiculo>> GuardarVehiculo(RequestVehiculo vehiculo)
        {
            try
            {
                var vehiculoGuardar = new Vehiculo
                {
                    Id = vehiculo.Id,
                    Modelo = vehiculo.Modelo
                };

                var savedData = await context.Vehiculos.AddAsync(vehiculoGuardar);
                await context.SaveChangesAsync();

                var response = new ResponseVehiculo
                {
                    Code = 200,
                    Message = "Vehiculo guardado correctamente",
                    Error = false,
                    VehiculoGuardado = new Vehiculo
                    {
                        Id = savedData.Entity.Id,
                        Modelo = savedData.Entity.Modelo
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("eliminarVehiculo/{id}")]
        public async Task<ActionResult<ResponseGenerico>> EliminarVehiculo(int id)
        {
            try
            {
                var vehiculo = await context.Vehiculos.FindAsync(id);
                if (vehiculo == null)
                {
                    return NotFound();
                }

                context.Vehiculos.Remove(vehiculo);
                await context.SaveChangesAsync();

                return Ok(new ResponseGenerico
                {
                    Code = 200,
                    Message = "Vehiculo eliminado correctamente",
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("actualizarVehiculo/{id}")]
        public async Task<ActionResult<ResponseVehiculo>> ActualizarVehiculo(int id, RequestVehiculo vehiculo)
        {
            try
            {
                if (id != vehiculo.Id)
                {
                    return BadRequest();
                }

                var vehiculoActualizar = await context.Vehiculos.FindAsync(id);
                if (vehiculoActualizar == null)
                {
                    return NotFound();
                }

                vehiculoActualizar.Modelo = vehiculo.Modelo;

                await context.SaveChangesAsync();

                return Ok(new ResponseVehiculo
                {
                    Code = 200,
                    Message = "Vehiculo actualizado correctamente",
                    Error = false,
                    VehiculoGuardado = new Vehiculo
                    {
                        Id = vehiculoActualizar.Id,
                        Modelo = vehiculoActualizar.Modelo
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("buscarVehiculo/{id}")]
        public async Task<ActionResult<Vehiculo>> BuscarVehiculo(int id)
        {
            var vehiculo = await context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        // Método para obtener un vehículo por su ID
        [HttpGet]
        [Route("obtenerVehiculoPorId/{id}")]
        public async Task<ActionResult<Vehiculo>> ObtenerVehiculoPorId(int id)
        {
            var vehiculo = await context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }
    }
}
