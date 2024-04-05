
using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore; //EntityFrameworkCore

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


        [HttpGet("listaPersonasRegistradasEnoc")]
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

        [HttpPost("guardarEnoc")]
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

        [HttpGet("listaPersonaEnoc")]
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
        [HttpPut("actualizarPersonaEnoc")]
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

        [HttpDelete("eliminarPersonaEnoc")]
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
        public async Task<IActionResult> guardarEnDB(RequestPersona persona)
        {
            try
            {
                Persona personaNueva = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido
                }; 
                await context.Persona.AddAsync(personaNueva);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //API BARRRIOS PERSONA ------------------------------------------------------------------------------------------------------------------




    }
}
