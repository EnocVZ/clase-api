using Azure;
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using System.Linq;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ILogger<PersonaController> logger;
        private readonly PersonaContext context;

        //esto es nuevo
        private List<Persona> listPersona = new List<Persona>();

        public PersonaController(ILogger<PersonaController> paramLogger, PersonaContext personaContext) {
            logger = paramLogger;
            context = personaContext;

            //Ejemplos de personas agregadas -< esto es nuevo
            //listPersona.Add(new Persona { Id = 1, Nombre = "Jose" , Apellido = "Prado"});
            //listPersona.Add(new Persona { Id = 2, Nombre = "Julio", Apellido = "Ramirez"});
        }

        [HttpGet("listaPersonasRegistradas")]
        public List<Persona> listaPersonasRegistradas()
        {
            List<Persona> listPersona = new List<Persona>();

            for (int i = 1; i <= 10; i++)
            {
                Persona persona = new Persona
                {
                    Id = i,
                    Nombre = "Persona" + i,
                    Apellido = "Apellido" + i,
                };
                listPersona.Add(persona);

            } // -> esto si sirve

            return listPersona;
        }

        [HttpPost("guardar")]

        public ResponsePostPersona Guardar(Persona persona)
        {
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            listaPersona.Add(persona);

            //se agrega a la persona a la lista global -> esto es nuevo
            //listPersona.Add(persona);
            
            /*List<Persona> list = new List<Persona>();
            
            Persona juan = new Persona
            {
                
                Nombre = "Juan"
            };
            list.Add(juan);
            list.Add(persona); //esto si sirve
            */

            ResponsePostPersona response = new ResponsePostPersona();
            if(persona.Id == null)
            {
                response.code = 200;
                response.error = false;
                //response.persona = persona;
                response.message = "Se agrego";
                persona.Id = listaPersona.Count + 1;
            }
            else
            {
                response.code = 404;
                response.error = true;
                response.message = "No se inserto";
            }
            response.listaPersona = listaPersona; //-> esto si sirve

            return response;
        }
        
        //devolver el objeto de la persona con el id que se manda en parametro

        [HttpGet("listaPersona")]
        public ResponseGetPersona listaPersona(int id)
        {
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            ResponseGetPersona response = new ResponseGetPersona();
            //esto es nuevo
            /*Persona personaEncontrada = listPersona.FirstOrDefault(p => p.Id == id);
            if(personaEncontrada != null)
            {
                response.personaEncontrada = personaEncontrada;
            }
            else
            {
                response.message = "Persona no encontrada";
            }*/

            //Persona personaEncontrada = null; -> esto no se que onda, lo vimos a medias ayer

            //List<Persona> listPersona = new List<Persona>();
            Persona personaEncontrada = new Persona();

            for (int i = 0; i < listaPersona.Count; i++)
            {
                Persona item = listaPersona[i];
                if(item.Id == id)
                {
                   personaEncontrada = item;
                }
            }
            //response.listaPersona = listPersona;

            //Response response = new Response();

            //personaEncontrada = listPersona.Where(x => x.Id == id).First();

            response.personaEncontrada = personaEncontrada; //esto si sirve
            
            return response;
        }

        //actualizar el nombre y apellido de la persona
        //devolver el id de la persona modificada
        [HttpPut("actualizarPersona")]
        public ResponsePutPersona actualizarPersona(Persona persona) { 
                
            List<Persona> listaPersona = this.listaPersonasRegistradas(); //-> esto si sirve
            ResponsePutPersona response = new ResponsePutPersona();
            Persona personaModificada = new Persona(); //-> esto si sirve
            /*Persona personaModificada = listPersona.FirstOrDefault(p => p.Id == persona.Id);
            if(personaModificada != null)
            {
                personaModificada.Nombre = persona.Nombre;  
                personaModificada.Apellido = persona.Apellido;
                response.message = "Persona actualizada";
                response.idPersona = personaModificada.Id;
                response.listaPersona = listPersona;
            }
            else
            {
                response.message = "Persona no encontrada";
            }*/


                for (int i = 0; i < listaPersona.Count; i++)
                {
                    if (listaPersona[i].Id == persona.Id)
                    {
                        personaModificada = listaPersona[i];
                        listaPersona[i].Nombre = persona.Nombre;
                        listaPersona[i].Apellido = persona.Apellido;
                        //personaModificada.Nombre = 

                    }
                }
                response.message = personaModificada.Nombre;
                response.idPersona = (int)personaModificada.Id;
                response.listaPersona = listaPersona; //Esto si sirve

                return response;
        }
        [HttpDelete("eliminarPersona")]

        public ResponsePostPersona eliminarPersona(int id)
        {
            ResponsePostPersona response = new ResponsePostPersona();
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            List<Persona> listaPersonaCopia = new List<Persona>();
            for (int i = 0; i < listaPersona.Count; i++) 
            {
                if (listaPersona[i].Id == id)
                {
                    listaPersonaCopia.Add(listaPersona[i]);
                    //listaPersona.Remove(listaPersona[i]);
                }
            }
            response.listaPersona = listaPersonaCopia;

            return response;
        }

        [HttpPost]
        [Route("guardarEnDB")]

        public async Task<ActionResult<IEnumerable<ResponseGetPersona>>> guardarEnDB(RequestPersona persona)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                Persona personaGuardar = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido
                };

                var savedData = await context.Persona.AddAsync(personaGuardar);
                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "Se guardo";
                response.error = false;
                response.personaEncontrada = new Persona
                {
                    Id = savedData.Entity.Id,
                    Nombre = savedData.Entity.Nombre,
                    Apellido = savedData.Entity.Apellido
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("listaPersonaDB")]

        public async Task<ActionResult<ResponseGetPersona>> listaPersonaDB()
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();


                List<Persona> savedData = await context.Persona.ToListAsync();
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Se muestra";
                response.error = false;
                response.listaPersona = savedData;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("actualizarPersonaDB")]

        public async Task<ActionResult<ResponseGetPersona>> actualizarPersonaDB(Persona persona)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                Persona personaEnBD = await context.Persona.FindAsync(persona.Id);
                personaEnBD.Nombre = persona.Nombre;
                personaEnBD.Apellido = persona.Apellido;
                await context.SaveChangesAsync();
                

                response.code = 200;
                response.message = "Se muestra";
                response.error = false;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("eliminarPersonaDB")]

        public async Task<ActionResult<ResponseGetPersona>> eliminarPersonaDB(int id)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var personaEnBD = await context.Persona.FindAsync(id);
                context.Remove(personaEnBD);
                await context.SaveChangesAsync();


                response.code = 200;
                response.message = "Se elimino";
                response.error = false;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("obtenerPersonabyID")]

        public async Task<ActionResult<ResponseGetPersona>> obtenerPersonabyID(int id)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                var personaEnBD = await context.Persona.FindAsync(id);
                await context.SaveChangesAsync();


                response.code = 200;
                response.message = "Se encontro";
                response.error = false;
                response.personaEncontrada = personaEnBD;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /*[HttpDelete]
        [Route("eliminarDeDB/{id}")]
        public async Task<IActionResult> EliminarDeDB(int id)
        {
            try
            {
                var personaAEliminar = await context.Persona.FindAsync(id);
                if (personaAEliminar == null)
                {
                    return NotFound();
                }

                context.Persona.Remove(personaAEliminar);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("listarDeDB")]
        public IActionResult ListarDeDB()
        {
            try
            {
                var personas = context.Persona.ToList();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }*/
    }
}