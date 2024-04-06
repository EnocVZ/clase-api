using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        //esto es nuevo
        private List<Persona> listPersona = new List<Persona>();

        public PersonaController(ILogger<PersonaController> paramLogger) {
            logger = paramLogger;

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
            //List<Persona> listaPersonaCopia = new List<Persona>();
            for (int i = 0; i < listaPersona.Count; i++) 
            {
                if (listaPersona[i].Id == id)
                {
                    //listaPersonaCopia.Add(listaPersona[i]);
                    listaPersona.Remove(listaPersona[i]);
                }
            }
            response.listaPersona = listaPersona;

            return response;
        }
    }
}