    using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("apiList/[controller]")]
    [ApiController]
    public class PersonaListController : ControllerBase
    {
        private readonly ILogger<PersonaController> logger;
        public PersonaListController(ILogger<PersonaController> paramLogger)
        {
            logger = paramLogger;
        }
        //GUARDAR EN CODIGO ###########################################################
        [HttpGet("listaPersonasRegistradasList")]
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

        [HttpPost("guardarPersonaList")]
        public ResponsePostPersona GuardarPersonaList(Persona persona)
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

        [HttpGet("listarPersonasList")]
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

        [HttpPut("actualizarPersonaList")]
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

        [HttpDelete("eliminarPersonaList")]
        public ResponsePostPersona eliminarPersonaEnoc(int id)
        {
            ResponsePostPersona response = new ResponsePostPersona();
            List<Persona> listaPersona = this.listaPersonasRegistradas();
            List<Persona> listaPersonaCopia = new List<Persona>();
            for (int i = 0; i < listaPersona.Count; i++)
            {
                if (listaPersona[i].Id != id)
                {
                    listaPersonaCopia.Add(listaPersona[i]);
                }
            }
            response.listaPersona = listaPersonaCopia;
            return response;
        }
    }
}
