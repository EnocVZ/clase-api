
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ILogger<PersonaController> logger;
        private readonly BDContext context;
        public PersonaController(ILogger<PersonaController> palogger, BDContext personaContext)
        {
            this.logger = palogger;
            context = personaContext;
        }

        //        [HttpPost("guardar")]
        //        public Response Guardar(Persona persona)
        //        {
        //            List<Persona> list = new List<Persona>();
        //            Persona juan = new Persona
        //            {
        //                Nombre = "juan"
        //            };
        //            list.Add(persona);
        //            list.Add(juan);

        //            Response response = new Response();
        //            if (persona.Id == 0)
        //            {
        //                response.code = 200;
        //                response.error = false;
        //                response.message = "se agrego";

        //            }
        //            else
        //            {
        //                response.code = 500;
        //                response.error = true;
        //                response.message = "No se pudoguardar";
        //            }
        //            response.listaPersona = list;

        //            return response;
        //        }

        //        [HttpGet("listaPersona2")]
        //        public ResponseGetPersona listaPersona(int id)
        //        {
        //            //Response response = new Response();
        //            //return response;
        //            ResponseGetPersona response = new ResponseGetPersona();
        //            List<Persona> listPersona = new List<Persona>();
        //            Persona luis = new Persona()
        //            {
        //                Id = 1,
        //                Nombre = "Luis",
        //            };
        //            Persona flor = new Persona()
        //            {
        //                Id = 2,
        //                Nombre = "Flor",
        //            };
        //            listPersona.Add(luis);
        //            listPersona.Add(flor);

        //            return response;
        //        }
        //        [HttpPut("actulizar")]
        //        public ResponsePutPersona actualizarPersona(Persona persona)
        //        {
        //            ResponsePutPersona response = new ResponsePutPersona();
        //            Persona enoc = new Persona
        //            {
        //                Id = 1,
        //                Nombre = "Enoc",
        //                Apellido = "Vazquez"
        //            };

        //            enoc.Nombre = persona.Nombre;
        //            enoc.Apellido = persona.Apellido;

        //            response.persona = enoc;
        //            response.IdPersonaModificada = enoc.Id;


        //            return response;
        //        }
        //        [HttpGet("listaPersonasRegistradas")]
        //        public List<Persona> listaPersonasRegistradas()
        //        {
        //            List<Persona> listPersona = new List<Persona>();

        //            for (int i = 1; i <= 10; i++)
        //            {
        //                Persona persona = new Persona
        //                {
        //                    Id = i,
        //                    Nombre = "Persona" + i,
        //                    Apellido = "Apellido" + i,
        //                };
        //                listPersona.Add(persona);

        //            }


        //            return listPersona;
        //        }
        //        [HttpPut("actualizarPersona")]
        //        public ResponsePutPersona aactualizarPersona(Persona persona)
        //        {

        //            List<Persona> listaPersona = this.listaPersonasRegistradas();
        //            ResponsePutPersona response = new ResponsePutPersona();
        //            Persona personaModificada = new Persona();

        //            for (int i = 0; i < listaPersona.Count; i++)
        //            {
        //                if (listaPersona[i].Id == persona.Id)
        //                {
        //                    personaModificada = listaPersona[i];

        //                    listaPersona[i].Nombre = persona.Nombre;
        //                    listaPersona[i].Apellido = persona.Apellido;
        //                    // personaModificada.Nombre = 
        //                }


        //            }


        //            response.message = personaModificada.Nombre;
        //            response.idPersona = (int)personaModificada.Id;
        //            response.listaPersona = listaPersona;




        //            return response;
        //        }
        [HttpPost]
        [Route("guardarEnDB")]
        public async Task<IActionResult> guardarEnDB(Persona persona)
        {
            try
            {
                // Crear una nueva instancia de Persona con los datos recibidos
                var nuevaPersona = new Persona()
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    // Agregar otros campos según sea necesario
                };

                // Agregar la nueva persona a la base de datos de forma asincrónica
                var result = await context.Persona.AddAsync(nuevaPersona);

                // Comprobar si la operación de agregar fue exitosa
                if (result == null)
                {
                    return BadRequest(); // Si no fue exitosa, retornar un error
                }

                // Guardar los cambios en la base de datos de forma asincrónica
                await context.SaveChangesAsync();

                // Retornar un código de éxito junto con la nueva persona creada
                return Ok(nuevaPersona);
            }
            catch (Exception ex)
            {
                // Manejar errores y excepciones
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("listarPersonaBD")]
        public async Task<ActionResult<ResponseGetPersona>> listarPersonaDB()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();

                List<Persona> personas = await context.Persona.ToListAsync();

                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Personas obtenidas correctamente";
                response.error = false;
                response.listaPersona = personas;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ModificarPersona")]
        public async Task<ActionResult<ResponseGetPersona>> ActualizarPersonaDB(Persona personaActualizada)
        {
            try
            {
                var personaEncontrada = await context.Persona.FindAsync(personaActualizada.Id);

                if (personaEncontrada != null)
                {
                    personaEncontrada.Nombre = personaActualizada.Nombre;
                    personaEncontrada.Apellido = personaActualizada.Apellido;

                    await context.SaveChangesAsync();

                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 200,
                        message = "Persona actualizada correctamente",
                        error = false,
                        personaEncontrada = personaEncontrada
                    };
                    return Ok(response);
                }
                else
                {
                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 404,
                        message = "Persona no encontrada",
                        error = true
                    };

                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("EliminarPersona")]
        public async Task<ActionResult<ResponseGetPersona>> EliminarPersonaDB(int Id)
        {
            try
            {
                var personaAEliminar = await context.Persona.FindAsync(Id);

                if (personaAEliminar != null)
                {
                    context.Persona.Remove(personaAEliminar);

                    await context.SaveChangesAsync();

                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 200,
                        message = "Persona eliminada correctamente",
                        error = false
                    };

                    return Ok(response);
                }
                else
                {
                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 404,
                        message = "Persona no encontrada",
                        error = true
                    };

                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("BuscarPorApellido")]
        public async Task<ActionResult<ResponseGetPersona>> BuscarPersonaPorApellido(string apellidoBusqueda)
        {
            try
            {
                List<Persona> personasEncontradas = await context.Persona.Where(p => p.Apellido == apellidoBusqueda).ToListAsync();

                if (personasEncontradas.Count > 0)
                {
                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 200,
                        message = $"Se encontraron personas con el apellido '{apellidoBusqueda}'",
                        error = false,
                        listaPersona = personasEncontradas
                    };

                    return Ok(response);
                }
                else
                {
                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 404,
                        message = $"No se encontraron personas con el apellido '{apellidoBusqueda}'",
                        error = true
                    };

                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("ModificarApellidoPersona")]
        public async Task<ActionResult<ResponseGetPersona>> ModificarApellidoPersona(int personaId, string nuevoApellido)
        {
            try
            {
                var personaEncontrada = await context.Persona.FindAsync(personaId);

                if (personaEncontrada != null)
                {
                    personaEncontrada.Apellido = nuevoApellido;

                    await context.SaveChangesAsync();

                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 200,
                        message = "Apellido de la persona modificado correctamente",
                        error = false,
                        personaEncontrada = personaEncontrada
                    };

                    return Ok(response);
                }
                else
                {
                    ResponseGetPersona response = new ResponseGetPersona
                    {
                        code = 404,
                        message = "Persona no encontrada",
                        error = true
                    };

                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
