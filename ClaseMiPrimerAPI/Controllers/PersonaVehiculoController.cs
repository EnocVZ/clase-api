using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly ILogger<PersonaVehiculoController> logger;
        private readonly PersonaContext context;
        public PersonaVehiculoController(ILogger<PersonaVehiculoController> paramLogger, PersonaContext personaContext)
        {
            logger = paramLogger;
            context = personaContext;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await context.PersonaVehiculo.ToListAsync();
                //var savedData = await context.PersonaVehiculo.ToListAsync();
                var listaPersona = await context.Persona.ToListAsync();
                var listaVehiculo = await context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                for (var i = 0; i <= listaPersonaVehiculo.Count; i++)
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(persona => persona.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(car => car.Id == personaVehiculo.IdVehiculo).FirstOrDefault();


                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Modelo = vehiculo.Modelo
                        };

                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
                /*var PersonaAuto = await context.PersonaVehiculo
                    .Join(context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido,
                    })
                    .ToListAsync();

                response.data = PersonaAuto;*/

                /*var personaVehiculo = await context.PersonaVehiculo
                    .Join(context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido
                    }).ToListAsync();

                var vehiculoPersona = await context.PersonaVehiculo
                    .Join(context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                    }).ToListAsync();*/

                var listPrueba = await context.PersonaVehiculo.Select(pv => new {
                    persona = context.Persona.FindAsync(pv.IdPersona),
                    vehiculo = context.Vehiculo.FindAsync(pv.IdVehiculo)
                }).ToListAsync();
                response.data = listaDatosPersonaVehiculo;

                //nuevo
                /*var AutoPersona = await context.PersonaVehiculo
                    .Join(context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                    })
                    .ToListAsync();

                response.data = AutoPersona;*/
                //aqui acaba

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Formulario para capturar el nombre de la persona con el vehiculo mas lujoso.

        [HttpPost]
        [Route("registrarPersonaVehiculo")]

        public async Task<ActionResult<Response>> registrarPersonaVehiculo(RequestPersonaVehiculo request)
        {
            try
            {
                Persona persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                };
                Vehiculo vehiculo = new Vehiculo
                {
                    Modelo = request.Modelo,
                };

                PersonaVehiculo personaVehiculo = new PersonaVehiculo();
                
                var savePersona = await context.Persona.AddAsync(persona);
                var saveVehiculo = await context.Vehiculo.AddAsync(vehiculo);
                await context.SaveChangesAsync();
                personaVehiculo.IdPersona = savePersona.Entity.Id;
                personaVehiculo.IdVehiculo = saveVehiculo.Entity.Id;
                await context.PersonaVehiculo.AddAsync(personaVehiculo);
                await context.SaveChangesAsync();

                Response response = new Response();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
