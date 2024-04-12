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
        public async Task<ActionResult<RelacionPersonaVehiculo>> listaRelacionesPersonasVehiculos()
        {
            try
            {
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();
                for (var i = 0; i < listaPersonaVehiculo.Count; i++) //numero de ids cantidad de realciones
                {
                    var personaVehiculoList = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(p => p.Id == personaVehiculoList.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(v => v.Id == personaVehiculoList.IdVehiculo).FirstOrDefault(); 
                    if(persona != null && vehiculo != null && personaVehiculoList != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculoList.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido, 
                            Modelo = vehiculo.Modelo, 
                            Color = vehiculo.Color, 
                            Uso = personaVehiculoList.Uso
                        };
                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
                /*
                _response.error = false;
                _response.code = 200;
                _response.data = listaDatosPersonaVehiculo;
                return Ok(_response); 
                */
                var personaVehiculo = await _context.PersonaVehiculo
                    .Join(_context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo,persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido
                    }).ToListAsync();

                var vehiculoPersona = await _context.PersonaVehiculo
                    .Join(_context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                        Color = vehiculo.Color
                    }).ToListAsync();
                _response.error = false;
                _response.code = 200;
                _response.data = listaDatosPersonaVehiculo;
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
        public async Task<ActionResult<ResponsePersonaVehiculo>> agregarRelacionPersonaVehiculo(RequestPersonaVehiculo requestPersonaVehiculo)
        {
            try
            {
                var idPersona = await _context.PersonaVehiculo.FindAsync(requestPersonaVehiculo.IdPersona); 
                var idVehiculo = await _context.PersonaVehiculo.FindAsync(requestPersonaVehiculo.IdVehiculo); 

                if(idPersona != null)
                {
                    _response.code = 500;
                    _response.message = "ERROR EN ID DE PERSONA. ";
                    _response.error = true;
                }

                if(idVehiculo != null)
                {
                    _response.code = 500;
                    _response.message = "ERROR EN ID DE VEHICULO. ";
                    _response.error = true;
                }
                else
                {
                    PersonaVehiculo personaVehiculo = new PersonaVehiculo
                    {
                        IdPersona = requestPersonaVehiculo.IdPersona,
                        IdVehiculo = requestPersonaVehiculo.IdVehiculo,
                        Uso = requestPersonaVehiculo.Uso
                    }; 
                    await _context.PersonaVehiculo.AddAsync(personaVehiculo);
                    await _context.SaveChangesAsync();

                    _response.code = 200;
                    _response.message = "Relacion agregada";
                    _response.error = false;
                    _response.RelacionPersonaVehiculo = personaVehiculo;
                }
                return Ok(_response);
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
