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
        private readonly BaseDatosContext _context;

        ResponsePersonaVehiculo _response = new ResponsePersonaVehiculo();

        public PersonaVehiculoController(BaseDatosContext _context)
        {
            this._context = _context;
        }




        [HttpGet]
        [Route("listaRelacionesPersonasVehiculos")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> listaRelacionesPersonasVehiculos()
        {
            try
            {
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPerosnaVehiculo = new List<DatosPersonaVehiculo>();


                for (var i = 0; i < listaPersonaVehiculo.Count; i++)
                {
                    var personaVehiculoXX = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(p => p.Id == personaVehiculoXX.IdPersona).FirstOrDefault();
                    if(persona != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido

                        };
                        listaDatosPerosnaVehiculo.Add(datosPersonaVehiculo);
                    }
                    
                }
                _response.error = false;
                _response.code = 200;
                _response.data = listaDatosPerosnaVehiculo;
                return Ok(_response); 
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                //Object reference not set to an instance of an object.

            }

        }




        [HttpPost]
        [Route("agregarRelacionPersonaVehiculo")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> agregarRelacionPersonaVehiculo(PersonaVehiculo personaVehiculo)
        {
            try
            {
                var idPersona = await _context.PersonaVehiculo.FindAsync(personaVehiculo.IdPersona); 

                var idVehiculo = await _context.PersonaVehiculo.FindAsync(personaVehiculo.IdVehiculo); 

                if(idPersona != null)
                {
                    _response.code = 500;
                    _response.message = "ERROR EN ID DE PERSONA. ";
                    _response.error = true;
                    return Ok(_response); //cambiar el return de lugar aqui termina, no debe temranr aqui 
                }
                /*
                 join 
                 */
                if(idVehiculo != null)
                {
                    _response.code = 500;
                    _response.message = "ERROR EN ID DE VEHICULO. ";
                    _response.error = true;
                    return Ok(_response); //cambiar el return de lugar 
                }
                else
                {
                    await _context.PersonaVehiculo.AddAsync(personaVehiculo);
                    await _context.SaveChangesAsync();

                    _response.code = 200;
                    _response.message = "Relacion agregada";
                    _response.error = false;
                    _response.RelacionPersonaVehiculo = personaVehiculo;
                    return Ok(_response);
                }
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarRelacionPersonaVehiculo")]
        public async Task<IActionResult> buscarRelacionPersonaVehiculo(int id)
        {
            PersonaVehiculo buscarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            
            if(buscarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada"; 
                _response.error = true;
                return Ok(_response); 
            }
            else
            {
                _response.code = 200;
                _response.message = "Relacion encontrada"; 
                _response.error = false;
                _response.RelacionPersonaVehiculo = buscarRelacion;
                return Ok(_response );
            }
        }

        [HttpPut]
        [Route("actualizarRelacionPersonaVehiculo")]
        public async Task<IActionResult> actualizarRelacionPersonaVehiculo (PersonaVehiculo personaVehiculo)
        {
            var buscarRelacion = await _context.PersonaVehiculo.FindAsync(personaVehiculo.Id); //consulta que debo agregar al metodo de agregar relacion 
            if(buscarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada";
                _response.error = true;
                return Ok(_response); 
            }
            else {
                buscarRelacion.IdPersona = personaVehiculo.IdPersona;
                buscarRelacion.IdVehiculo = personaVehiculo.IdVehiculo;
                buscarRelacion.Uso = personaVehiculo.Uso;
                await _context.SaveChangesAsync(); 

                _response.code = 200;
                _response.message = "Relacion actualizada. ";
                _response.error = false; 
                return Ok(_response);
            }
        }

        [HttpDelete]
        [Route("eliminarRelacionPersonaVehiculo")]
        public async Task<IActionResult> eliminarRelacionPersonaVehiculo (int id)
        {
            var eliminarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            if(eliminarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada"; 
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                /*            vehiculoContext.Vehiculo.Remove(vehiculoEliminar);
            await vehiculoContext.SaveChangesAsync(); */
                _context.PersonaVehiculo.Remove(eliminarRelacion);
                await _context.SaveChangesAsync();
                _response.code = 200;
                _response.message = "Relacion eliminada"; 
                _response.error = false;
                _response.RelacionPersonaVehiculo = eliminarRelacion; 
                return Ok(_response);
            }
        }


    }
}
