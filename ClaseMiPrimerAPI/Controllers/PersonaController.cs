using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ILogger<PersonaController> logger;
        private readonly Contextt context;
        //private readonly VehiculoContext contextv;
        public PersonaController(ILogger<PersonaController> paramLogger, Contextt personaContext)
        {
            logger = paramLogger;
            context = personaContext;
           // contextv = vehiculoContext;
            
        }
        /*

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

            }


            return listPersona;
        }
        */
        /*
        [HttpPost("guardar")]
        public ResponsePostPersona Guardar(Persona persona)
        {
            List<Persona> listaPersona = this.listaPersonasRegistradas();


            ResponsePostPersona response = new ResponsePostPersona();
            if (persona.Id == null)
            {
                response.code = 200;
                response.error = false;
                response.message = "Se agrego";
                persona.Id = listaPersona.Count + 1;
                listaPersona.Add(persona);

            }
            else
            {
                response.code = 404;
                response.error = true;
                response.message = "No se inserto";
            }
            response.listaPersona = listaPersona;

            return response;

        }
        */


        //devolver el objeto de la persona con el id que se manda en parametro
        /*
        [HttpGet("listaPersona")]
        public ResponseGetPersona listaPersona(int id)
        {
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            ResponseGetPersona response = new ResponseGetPersona();

            Persona personaEncontrada = new Persona();


            for (int i = 0; i < listaPersona.Count; i++)
            {
                Persona item = listaPersona[i];
                if (item.Id == id)
                {
                    personaEncontrada = item;

                }

            }

            response.personaEncontrada = personaEncontrada;

            return response;
        }
        */

        //actualizar el nombre y apellido de la persona en base a id
        //devolver el id de la persona modificada
        /*
        [HttpPut("actualizarPersona")]
        public ResponsePutPersona actualizarPersona(Persona persona)
        {

            List<Persona> listaPersona = this.listaPersonasRegistradas();
            ResponsePutPersona response = new ResponsePutPersona();
            Persona personaModificada = new Persona();

            for (int i = 0; i < listaPersona.Count; i++)
            {
                if (listaPersona[i].Id == persona.Id)
                {
                    personaModificada = listaPersona[i];

                    listaPersona[i].Nombre = persona.Nombre;
                    listaPersona[i].Apellido = persona.Apellido;
                    // personaModificada.Nombre = 
                }


            }


            response.message = personaModificada.Nombre;
            response.idPersona = (int)personaModificada.Id;
            response.listaPersona = listaPersona;


            return response;
        }
        */
        /*
        [HttpDelete("eliminarPersona")]
        public ResponsePostPersona eliminarPersona(int id)
        {
            ResponsePostPersona response = new ResponsePostPersona();
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            List<Persona> listaPersonaCopia = new List<Persona>();
            for (int i = 0; i < listaPersona.Count; i++)
            {
                if (listaPersona[i].Id != id)
                {
                    listaPersonaCopia.Add(listaPersona[i]);
                    // listaPersona.Remove(listaPersona[i]);

                }


            }
            response.listaPersona = listaPersonaCopia;

            return response;
        }
        */
        /*

        [HttpPost]
        [Route("guardarEnDB")]
        public async Task<IActionResult> guardarEnDB(RequestPersona persona)
        {
            try
            {
                Persona personaGuardar = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido
                };

                await context.Persona.AddAsync(personaGuardar);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */

        [HttpPost]
        [Route("guardarPersonaEnBD")]
        public async Task<ActionResult<ResponseGetPersona>> guardarPersonaEnBD(RequestPersona persona)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                Persona personaGuardar = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido
                };
                //var saveData = await context.Vehiculo.AddAsync(vehiculoGuardar);
                //var saveData = await contextv.Persona.AddAsync(personaGuardar);

                //await context.Persona.AddAsync(personaGuardar);
                var saveData = await context.Persona.AddAsync(personaGuardar);
                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "agregado";
                response.error = false;
                response.personaEncontrada = new Persona
                {
                    Id = saveData.Entity.Id,
                    Nombre = saveData.Entity.Nombre,
                    Apellido = saveData.Entity.Apellido,

                };

                // await context.Persona.AddAsync(personaGuardar);
                //await context.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //------------------------------------- EDITAR PERSONA---------------------------------------------------


        [HttpPut]
        [Route("actualizarPersonaEnDB")]
        public async Task<ActionResult<ResponseGetPersona>> actualizarPersonaEnDB(int id, RequestPersona persona)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                Persona personaEncontrada = await context.Persona.FindAsync(id);

                if (personaEncontrada == null)
                {
                    response.code = 404;
                    response.message = "No se encontró la persona con el id especificado";
                    response.error = true;
                    return NotFound(response);
                }

                personaEncontrada.Nombre=persona.Nombre;
                personaEncontrada.Apellido=persona.Apellido;

                context.Entry(personaEncontrada).State = EntityState.Modified;
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "actualizado";
                response.error = false;
                response.personaEncontrada = new Persona
                {
                    Id = personaEncontrada.Id,
                    Nombre = personaEncontrada.Nombre,
                    Apellido = personaEncontrada.Apellido,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //------------------------------------------------------------ BUSCAR PERSONA --------------------------------------------------------
        [HttpGet]
        [Route("buscarPersonaBD")]
        public async Task<ActionResult<ResponseGetPersona>> buscarPersonaBD(int id)
        {
            ResponseGetPersona response = new ResponseGetPersona();
            Persona personaEncontrada = await context.Persona.FindAsync(id);

            if (personaEncontrada == null)
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
            return Ok(personaEncontrada);

        }


        //------------------------------------------------------------- ELIMINAR PERSONA ------------------------------------


        [HttpDelete]
        [Route("eliminarPersonaEnDB")]
        public async Task<ActionResult<ResponseGetPersona>> eliminarPersonaEnDB(int id)
        {
            try
            {
                ResponseGetPersona response = new ResponseGetPersona();
                Persona personaEncontrada = await context.Persona.FindAsync(id);

                if (personaEncontrada == null)
                {
                    response.code = 404;
                    response.message = "No se encontró el vehiculo ese ID";
                    response.error = true;
                    return NotFound(response);
                }

                context.Persona.Remove(personaEncontrada);
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Persona eliminada correctamente";
                response.error = false;
                response.personaEncontrada = new Persona
                {
                    Id = personaEncontrada.Id,
                    Nombre = personaEncontrada.Nombre,
                    Apellido = personaEncontrada.Apellido,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


      
        //----------------------------------------------- BUSQUEDA POR NOMBRE Y APELLIDO -------------------------------------------



        [HttpGet]
        [Route("buscarPersonaPorNombreOApellido")]
        public async Task<ActionResult<IEnumerable<Persona>>> BuscarPersonaPorNombreOApellido(string nombre, string apellido)
        {
            try
            {
                IEnumerable<Persona> personas = await context.Persona
                    .Where(p => p.Nombre.Contains(nombre) || p.Apellido.Contains(apellido))
                    .ToListAsync();

                return Ok(personas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




       

        //---------------------------------------------- RELACIONAR EN UNA TABLA LOS ID DE VEHICULO Y PERSONA --------------------------------------------








    }
}