using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Controllers;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;
        private readonly PersonaContext context;
        public VehiculoController(ILogger<VehiculoController> paramLogger, PersonaContext personaContext)
        {
            this.logger = paramLogger;
            context = personaContext;
        }
        [HttpPost("guardar")]
        public ResponsePostVehiculo Guardar(Vehiculo vehiculo)
        {


            ResponsePostVehiculo response = new ResponsePostVehiculo();
            if (vehiculo.Id == 0)
            {
                response.code = 200;
                response.error = false;
                response.message = "Su Vehiculo se a agregado.";


            }
            else
            {
                response.code = 500;
                response.error = true;
                response.message = "Su Vehiculo no se a podido agregar";
            }
            response.vehiculo = vehiculo;

            return response;

        }

        [HttpGet("listVehiculo")]

        public List<Vehiculo> listVehiculo()
        {
            List<Vehiculo> listVehiculo = new List<Vehiculo>();
            Vehiculo vehiculo = new Vehiculo
            {
                
                Modelo = "X-TRAIL E-POWER",
                Anio = 2023,
                Marca = "Nissan",
                
            };
           
            listVehiculo.Add (vehiculo);
            return listVehiculo;

        }
        [HttpDelete]
         [HttpPost]
            [Route("guardarEnDB")]
            public async Task<IActionResult> guardarEnDB(Vehiculo vehiculo)
            {

                try
                {
                var vehiculoA = new Vehiculo()
                {
                    Id = 0,
                    Modelo = vehiculo.Modelo,
                    Anio = vehiculo.Anio,
                    Marca = vehiculo.Marca
                };
            
                 var result = await context.Vehiculo.AddAsync(vehiculoA);
                if(result == null)
                {
                    return BadRequest();
                }
                    await context.SaveChangesAsync();
               
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
    }
}
