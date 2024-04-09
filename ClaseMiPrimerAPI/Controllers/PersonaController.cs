
using Azure;
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
        private readonly PersonaContext context;
        public PersonaController(ILogger<PersonaController> paramLogger, PersonaContext personaContext) {
            logger = paramLogger;
            context = personaContext;
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

            }


            return listPersona;
        }

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



        //devolver el objeto de la persona con el id que se manda en parametro

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


        //actualizar el nombre y apellido de la persona en base a id
        //devolver el id de la persona modificada
        [HttpPut("actualizarPersona")]
        public ResponsePutPersona actualizarPersona(Persona persona) {

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
                //metodos asincronos y sincronos esto es lo esencial para guardar
                var savedData = await context.Persona.AddAsync(personaGuardar);
                await context.SaveChangesAsync();
               
                response.code = 200;
                response.message = "Se guardo";
                ; response.error = false;
                response.personaEncontrada = new Persona
                {// esto es una forma de acceder se utliza para recuperar un id que se inserto y recuperarlo
                    Id = savedData.Entity.Id,
                    Nombre = savedData.Entity.Nombre,
                    Apellido = savedData.Entity.Apellido,

                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("listaPersonaBD")]
        //busqueda
        public async Task<ActionResult<IEnumerable<ResponseGetPersona>>> listaPersonaBD()
        {
            try
            {// de aca parte la busqueda
                ResponseGetPersona response = new ResponseGetPersona();
                //metodos asincronos y sincronos
                List<Persona> savedData = await context.Persona.ToListAsync();
                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "Se guardo";
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
        [Route("ActualizarPersonaBD")]
        //busqueda
        public async Task<ActionResult<IEnumerable<ResponseGetPersona>>> actuzalizarPersonaBD(int id, Persona persona)
        {
            try
            {// de aca parte la busqueda
                ResponseGetPersona response = new ResponseGetPersona();

                Persona personaEnBD = await context.Persona.FindAsync(persona.Id);
                personaEnBD.Nombre = persona.Nombre;
                personaEnBD.Apellido = persona.Apellido;

                await context.SaveChangesAsync();
                response.code = 200;
                response.message = "Se guardo";
                response.error = false;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("eliminarPersonaBD")]
        //busqueda
        public async Task<ActionResult<ResponseGetPersona>> eliminarPersonaBD(int id)
        {
            try
            {// de aca parte la busqueda
                ResponseGetPersona response = new ResponseGetPersona();

                var personaEnBD = await context.Persona.FindAsync(id);
                context.Remove(personaEnBD);
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Se ellimino";
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
        //busqueda
        public async Task<ActionResult<ResponseGetPersona>> obtenerPersonabyID(int id)
        {
            try
            {// de aca parte la busqueda
                ResponseGetPersona response = new ResponseGetPersona();
                var personaEnBD = await context.Persona.FindAsync(id);
                await context.SaveChangesAsync();

                response.code = 200;
                response.message = "Se ellimino";
                response.error = false;
                // esto hace que se busque el id 
                response.personaEncontrada = personaEnBD;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

}
