using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonaVehiculoController : Controller
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly BDContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> palogger, BDContext BDContext)
        {
            this.logger = palogger;
            context = BDContext;
        }
        [HttpGet]
        [Route("ObtenerPersonaVehiculo")]
        public async Task<ActionResult<List<PersonaVehiculoController>>> listaPersonaBD()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var saveData = await context.PersonaVehiculo.ToListAsync();
                return Ok(saveData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost]
        [Route("GuardarPersonaVehiculo")]

        public async Task<ActionResult<Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
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
                    Anio = request.Anio
                };
                PersonaVehiculo personaVehiculo = new PersonaVehiculo();

                var savePersona = await context.Persona.AddAsync(persona);
                var saveVehiculo = await context.Vehiculo.AddAsync(vehiculo);
                await context.SaveChangesAsync();
                personaVehiculo.IdPersona = savePersona.Entity.Id;
                personaVehiculo.IdVehiculo = saveVehiculo.Entity.id;
                await context.PersonaVehiculo.AddAsync(personaVehiculo);
                await context.SaveChangesAsync();
                Response response = new Response();
                return Ok(response);
            
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }








        //public async Task<ActionResult<IEnumerable<PersonaVehiculo>>> ObtenerPersonaVehiculo()
        //{
        //    try
        //    {
        //        var personaVehiculo = from persona in context.Persona
        //                              join vehiculo in context.Vehiculo on persona.Id equals vehiculo.id
        //                              select new PersonaVehiculo { };


        //        var personaVehiculoList = await personaVehiculo.ToListAsync();

        //        return Ok(personaVehiculoList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }

        //}
        //[HttpPost]
        //[Route("CrearPersonaVehiculo")]
        //public async Task<ActionResult<PersonaVehiculo>> CrearPersonaVehiculo(PersonaVehiculo nuevoPersonaVehiculo)
        //{
        //    try
        //    {
        //        // Verificar si el modelo recibido es válido
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }


        //        var personaVehiculo = new PersonaVehiculo
        //        {


        //        };


        //        context.PersonaVehiculo.Add(personaVehiculo);
        //        await context.SaveChangesAsync();

        //        return Ok(personaVehiculo); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex); 
        //    }
        //}
    }

}

