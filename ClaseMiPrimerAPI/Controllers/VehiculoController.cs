using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController: ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;
        // private readonly PersonaContext context;
        private readonly Contextt context;
        public VehiculoController(ILogger<VehiculoController> paramLogger, Contextt vehiculoContext)
        {
            logger = paramLogger;
            //context = personaContext;
            context = vehiculoContext;

        }

        //----------------------------------------------- GUARDAR VEHICULOS ---------------------------------------------


        [HttpPost]
        [Route("guardarVehiculoEnDB")]
        public async Task<ActionResult<ResponseGetVehiculo>> guardarVehiculosEnBD(RequestVehiculo vehiculo)
        {
            try
            {
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                Vehiculo vehiculoGuardar = new Vehiculo
                {
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo
                };
                //var saveData = await context.Vehiculo.AddAsync(vehiculoGuardar);
                var saveData = await context.Vehiculo.AddAsync(vehiculoGuardar);
                //await context.Persona.AddAsync(personaGuardar);
                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "agregado";
                response.error = false;
                response.vehiculoEncontrado = new Vehiculo
                {
                    Id = saveData.Entity.Id,
                    Marca = saveData.Entity.Marca,
                    Modelo = saveData.Entity.Modelo,

                };

                // await context.Persona.AddAsync(personaGuardar);
                //await context.SaveChangesAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //------------------------------ EDITAR VEHICULOS -----------------------------------------------------


        [HttpPut]
        [Route("actualizarVehiculosEnDB")]
        public async Task<ActionResult<ResponseGetVehiculo>> actualizarVehiculosEnDB(int id, RequestVehiculo vehiculo)
        {
            try
            {
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                Vehiculo vehiculoEncontrado = await context.Vehiculo.FindAsync(id);

                if (vehiculoEncontrado == null)
                {
                    response.code = 404;
                    response.message = "No se encontró la persona con el id especificado";
                    response.error = true;
                    return NotFound(response);
                }

                vehiculoEncontrado.Marca = vehiculo.Marca;
                vehiculoEncontrado.Modelo = vehiculo.Modelo;

                context.Entry(vehiculoEncontrado).State = EntityState.Modified;
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "actualizado";
                response.error = false;
                response.vehiculoEncontrado = new Vehiculo
                {
                    Id = vehiculoEncontrado.Id,
                    Marca = vehiculoEncontrado.Marca,
                    Modelo = vehiculoEncontrado.Modelo,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //------------------------------------------- BUSCAR VEHICULOS --------------------------------------------------------


        [HttpGet]
        [Route("buscarVehiculoPorId")]
        public async Task<ActionResult<ResponseGetVehiculo>> BuscarVehiculoPorId(int id)
        {
            ResponseGetVehiculo response = new ResponseGetVehiculo();
            Vehiculo vehiculoEncontrado = await context.Vehiculo.FindAsync(id);

            if (vehiculoEncontrado == null)
            {
                response.code = 404;
                response.message = "No se encontró el vehiculo con ese ID";
                response.error = true;

            }
            else
            {
                response.code = 200;
                response.message = "Vehiculo encontrado";
                response.error = false;


            }
            return Ok(vehiculoEncontrado);

        }


        //--------------------------------------------- ELIMINAR VEHICULOS DE LA BD ---------------------------------------------------------------


        [HttpDelete]
        [Route("eliminarVehiculoEnDB")]
        public async Task<ActionResult<ResponseGetVehiculo>> eliminarVehiculoEnDB(int id)
        {
            try
            {
                ResponseGetVehiculo response = new ResponseGetVehiculo();
                Vehiculo vehiculoEncontrado = await context.Vehiculo.FindAsync(id);

                if (vehiculoEncontrado == null)
                {
                    response.code = 404;
                    response.message = "No se encontró el vehiculo ese ID";
                    response.error = true;
                    return NotFound(response);
                }

                context.Vehiculo.Remove(vehiculoEncontrado);
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Persona eliminada correctamente";
                response.error = false;
                response.vehiculoEncontrado = new Vehiculo
                {
                    Id = vehiculoEncontrado.Id,
                    Marca = vehiculoEncontrado.Marca,
                    Modelo = vehiculoEncontrado.Modelo,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //----------------------------------------------- BUSQUEDA POR MARCA Y MODELO -------------------------------------------



        [HttpGet]
        [Route("buscarVehiculoPorMarcaOModelo")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> BuscarVehiculoPorMarcaOModelo(string marca, string modelo)
        {
            try
            {
                IEnumerable<Vehiculo> vehiculos = await context.Vehiculo
                    .Where(v => v.Marca.Contains(marca) || v.Modelo.Contains(modelo))
                    .ToListAsync();

                return Ok(vehiculos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //------------------------------ LISTA --------------------------------------
        [HttpGet]
        [Route("lista")]

        public async Task<ActionResult<List<Vehiculo>>> lista()
        {
            try
            {
                List<Vehiculo> dataList = await context.Vehiculo.ToListAsync();
                await context.SaveChangesAsync();

                return Ok(dataList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
