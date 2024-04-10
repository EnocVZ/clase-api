using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using ClaseMiPrimerAPI.DbListContext;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;
        private readonly DBContext context;
        public VehiculoController(ILogger<VehiculoController> paramLogger, DBContext personaContext)
        {
            logger = paramLogger;
            context = personaContext;
        }


        //_______________________________________________________________________________________________

        [HttpPost]
        [Route("guardarVehiculoDB")]
        public async Task<ActionResult<IEnumerable<ResponseGetVehiculo>>> guardarVehiculoDB(RequestVehiculo vehiculo)
        {
            try
            {
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                Vehiculo VehiculoGuardar = new Vehiculo
                {
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo,
                    Anio = vehiculo.Anio,
                    Color = vehiculo.Color
                };

                var savedData = await context.Vehiculo.AddAsync(VehiculoGuardar);
                await context.SaveChangesAsync();

                logger.LogInformation("Se ha guardado un vehículo con éxito.");

                response.code = 200;
                response.message = "Se guardo con éxito el Vehiculo";
                response.error = false;
                response.vehiculoEncontrado = new Vehiculo
                {
                    Id = savedData.Entity.Id,
                    Marca = savedData.Entity.Marca,
                    Modelo = savedData.Entity.Modelo,
                    Anio = savedData.Entity.Anio,
                    Color = savedData.Entity.Color
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Error al guardar el vehículo: " + ex.Message);
                return BadRequest("Error al guardar el vehículo: " + ex.Message);
            }
        }

        //_______________________________________________________________________________________________-


        [HttpGet]
        [Route("listaVehiculosBD")]
        //busqueda se maneja un get para optener la lista
        //Define el método listaVehiculosBD como un método asincrónico que devuelve una lista de objetos ResponseGetVehiculo 
        public async Task<ActionResult<IEnumerable<ResponseGetVehiculo>>> listaVehiculosBD()
        {
            try
            {// de aca parte la busqueda
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                //metodos asincronos y sincronos
                //Realiza una consulta asincrónica a la base de datos para obtener todos los vehículos y los guarda en savedData.
                List<Vehiculo> savedData = await context.Vehiculo.ToListAsync();
                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "Se muetra la lista con exito";
                response.error = false;
                response.listaVehiculo = savedData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //_______________________________________________________________________________________________
        [HttpPut]
        [Route("ActualizarVehiculoBD")]
        //busqueda
        public async Task<ActionResult<IEnumerable<ResponseGetVehiculo>>> ActualizarVehiculoBD(int id, Vehiculo vehiculo)
        {
            try
            {// de aca parte la busqueda
                ResponseGetVehiculo response = new ResponseGetVehiculo();

                Vehiculo vehiculoEnBD = await context.Vehiculo.FindAsync(vehiculo.Id);
                vehiculoEnBD.Marca = vehiculo.Marca;
                vehiculoEnBD.Modelo = vehiculo.Modelo;

                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "Se actuaLizo con exito";
                response.error = false;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //_______________________________________________________________________________________________
        [HttpDelete]
        [Route("eliminarVehiculoBD")]
        //busqueda
        public async Task<ActionResult<ResponseGetVehiculo>> eliminarVehiculoBD(int id)
        {
            try
            {// de aca parte la busqueda
                ResponseGetVehiculo response = new ResponseGetVehiculo();

                var vehiculoEnBD = await context.Vehiculo.FindAsync(id);
                context.Remove(vehiculoEnBD);
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Se elimino con exito";
                response.error = false;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //_______________________________________________________________________________________________
        [HttpGet]
        [Route("obtenerVehiculobyID")]
        //busqueda
        public async Task<ActionResult<ResponseGetVehiculo>> obtenerVehiculobyID(int id)
        {
            try
            {// de aca parte la busqueda
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                var vehiculoEnBD = await context.Vehiculo.FindAsync(id);
                
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Se obtuvo vehiculo con exito";
                response.error = false;
                // esto hace que se busque el id 
                response.vehiculoEncontrado = vehiculoEnBD;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //_______________________________________________________________________________________________
    }
}

 