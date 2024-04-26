using Azure;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;

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

                await context.SaveChangesAsync(); // Guardar cambios una vez después de agregar las entidades

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


        /*
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<DatosPersonaVehiculo>>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await context.PersonasVehiculo.ToArrayAsync();
                var listaPersona = await context.Persona.ToArrayAsync();
                var listaVehiculo = await context.Vehiculo.ToArrayAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                var personaAuto = await context.PersonasVehiculo
                .Join(context.Persona, personaVehiculo => personaVehiculo.IdPersona, persona => persona.Id, (joined,persona)); => { personaVehiculo}
                .Join(context.Vehiculo, joined => joined.personsaVehiculo.IdPersona, vehiculo => vehiculo.Id, (joined, vehiculo) => new DatosPersonaVehiculo)
                 {
                    IdPersonaVehiculo = joined.personaVehiculo.Id,
                            Nombre = joined.persona.Nombre,
                            Apellido = joined.persona.Apellido,
                            Marca = joined.vehiculo.Marca,
                            Modelo = joined.vehiculo.Modelo
                        })
                    ToListAsync();
                var listpersona = await context.PersonasVehiculo.Select()pev => new {

                    personaVehiculo = pv.
                    persona = context.Persona.Where(persona = > persona.Id == pv.Idpersona).First(),
                    vehiculo = context.Vehiculo.Where(Vehiculo => Vehiculo.Id == PV.IdVehiculo).First()
                }).Select(DatosPersonaVehiculo => new DatosPersonaVehiculo)
                {

                    IdPersonaVehiculo = datos.personaVheiculo.Id,
                Nombre = datos.persona.Nombre,
                Apellido = datos.persona.Apellido,
                Marca = datos.vehiculo.Marca,
                Modelo = datos.vehiculo.Modelo


            }).ToListAsync();
                response.data = listaDatosPersonaVehiculo;





                return Ok(PersonaAuto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.mensaage)
                }
        }*/
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<DatosPersonaVehiculo>>> Lista()
        {
            try
            {
                // Consider using eager loading or projection for efficiency
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await context.PersonasVehiculo.ToListAsync();
                var listaPersona = await context.Persona.ToListAsync();
                var listaVehiculo = await context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();
                
                 
               

                var personaAuto = await context.PersonasVehiculo
                  .Join(context.Persona, personaVehiculo => personaVehiculo.IdPersona, persona => persona.Id,(personaVehiculo, persona) => personaVehiculo)
                   .Join(context.Vehiculo, joined => joined.IdPersona, vehiculo => vehiculo.Id, (joined, vehiculo) => new DatosPersonaVehiculo
                {
                    IdPersonaVehiculo = joined.Id,
                    Nombre = joined.Persona.Nombre,
                    Apellido = joined.Persona.Apellido,
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo
                })
            .ToListAsync();

                var listPrueba = await context.PersonasVehiculo.Select(pv => new {

                    personaVehiculo = pv,
                    persona = context.Persona.Where(persona => persona.Id == pv.IdPersona).First(),
                    vehiculo = context.Vehiculo.Where(Vehiculo => Vehiculo.Id == pv.IdVehiculo).First()
                }).Select(datos => new DatosPersonaVehiculo
                {

                IdPersonaVehiculo = datos.personaVehiculo.Id,
                Nombre = datos.persona.Nombre,
                Apellido = datos.persona.Apellido,
                Marca = datos.vehiculo.Marca,
                Modelo = datos.vehiculo.Modelo


            }).ToListAsync();
                


                return Ok(listaDatosPersonaVehiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = " Error" });
            }
        }


       /* [HttpGet]
        [Route("listaPersonaVehiculo")]
        public async Task<ActionResult<List<DatosPersonaVehiculo>>> listaPersonaVehiculo()
        {
            try
            {
                var listaDatosPersonaVehiculo = await context.PersonasVehiculo
                    .Include(pv => pv.Persona)
                    .Include(pv => pv.Vehiculo)
                    .Select(pv => new DatosPersonaVehiculo
                    {
                        IdPersonaVehiculo = pv.Id,
                        Nombre = pv.Persona.Nombre,
                        Apellido = pv.Persona.Apellido,
                        Marca = pv.Vehiculo.Marca,
                        Modelo = pv.Vehiculo.Modelo
                    })
                    .ToListAsync();

                return Ok(listaDatosPersonaVehiculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


*/







    }
}

