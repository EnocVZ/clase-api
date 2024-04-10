using Azure;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly DBContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, DBContext baseContext)
        {
            logger = paramLogger;
            context = baseContext;
        }

        //_________________________________________________________




        [HttpGet]
        [Route("listaPersonaBD")]
        //busqueda
        public async Task<ActionResult<List<PersonaVehiculo>>> listaPersonaBD()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var savedData = await context.PersonasVehiculo.ToListAsync();
                return Ok(savedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("listaPersona")]
        public async Task<ActionResult<List<PersonaVehiculo>>> listaPersona()
        {
            try
            {
                var personavehiculo = await context.PersonasVehiculo.ToListAsync();
                 
                var response = personavehiculo.Select(pv => new PersonaVehiculo
                {
                    Id = pv.Id,
                    IdPersona = pv.IdPersona,
                    IdVehiculo = pv.IdVehiculo,
                    Nombre = pv.Nombre, // Accede al nombre de la persona a través de la propiedad de navegación
                    Marca = pv.Marca
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }









        //Formulario para capturar el nombre de la persona con el vehiculo mas lujoso

        [HttpPost]
        [Route("registrarPersonaVehiculo")]
        public async Task<ActionResult<ClaseMiPrimerAPI.view.Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
        {
            try
            {
                Persona persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido
                };

                Vehiculo vehiculo = new Vehiculo
                {
                    Marca = request.Marca,
                    Modelo = request.Modelo,
                    Anio = request.Anio,
                    Color = request.Color
                };

                await context.Persona.AddAsync(persona);
                await context.Vehiculo.AddAsync(vehiculo);
                await context.SaveChangesAsync();

                PersonaVehiculo personaVehiculo = new PersonaVehiculo
                {
                    IdPersona = persona.Id,
                    IdVehiculo = vehiculo.Id
                };

                await context.PersonasVehiculo.AddAsync(personaVehiculo);
                await context.SaveChangesAsync();

                ClaseMiPrimerAPI.view.Response response = new ClaseMiPrimerAPI.view.Response
                {
                    code = 200,
                    message = "Persona y vehículo registrados correctamente",
                    error = false
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ClaseMiPrimerAPI.view.Response
                {
                    code = 500,
                    message = ex.Message,
                    error = true
                });
            }
        }
    [HttpGet]
        [Route("lista")]
        //busqueda
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

