

using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVehiculoController : ControllerBase
    {
        private readonly Contextt _context;
        ResponsePersonaVehiculo _response = new ResponsePersonaVehiculo();

        public PersonaVehiculoController(Contextt contextv)
        {
            _context = contextv;
        }
       /*
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<RequestPersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                //var savedData = await context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                for (var i = 0; i <= listaPersonaVehiculo.Count; i++)
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(persona => persona.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(car => car.Id == personaVehiculo.IdVehiculo).FirstOrDefault();


                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Modelo = vehiculo.Modelo
                        };

                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
                var PersonaAuto = await _context.PersonaVehiculo
                    .Join(_context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido
                    })
                    .ToListAsync();

                response.data = PersonaAuto;

                //nuevo
                var AutoPersona = await _context.PersonaVehiculo
                    .Join(_context.Vehiculo, pV => pV.IdVehiculo, p => p.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                        Modelo = vehiculo.Modelo,
                    })
                    .ToListAsync();

                response.data = AutoPersona;
                //aqui acaba

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       */

        [HttpGet]
        [Route("listaRelacionesPersonasVehiculos")]
        public async Task<ActionResult<RequestPersonaVehiculo>> listaRelacionesPersonasVehiculos()
        {
            try
            {
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();
                for (var i = 0; i < listaPersonaVehiculo.Count; i++) //numero de ids cantidad de realciones
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(p => p.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(v => v.Id == personaVehiculo.IdVehiculo).FirstOrDefault();
                    if (persona != null && vehiculo != null && personaVehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Marca= vehiculo.Marca,
                            Modelo = vehiculo.Modelo,
                           // Color = vehiculo.Color
                        };
                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }
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
        /*
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<ResponsePersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                for (var i = 0; i < listaPersonaVehiculo.Count; i++)
                {
                    var personaVehiculo = listaPersonaVehiculo[i];
                    var persona = listaPersona.Where(persona => persona.Id == personaVehiculo.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(car => car.Id == personaVehiculo.IdVehiculo).FirstOrDefault();

                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Marca = vehiculo.Marca,
                            Modelo = vehiculo.Modelo
                        };

                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }

                response.data = listaDatosPersonaVehiculo;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        */
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<RequestPersonaVehiculo>> lista()
        {
            try
            {
                ResponsePersonaVehiculo response = new ResponsePersonaVehiculo();
                var listaPersonaVehiculo = await _context.PersonaVehiculo.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                List<DatosPersonaVehiculo> listaDatosPersonaVehiculo = new List<DatosPersonaVehiculo>();

                foreach (var personaVehiculo in listaPersonaVehiculo)
                {
                    var persona = listaPersona.FirstOrDefault(pv => pv.Id == personaVehiculo.IdPersona);
                    var vehiculo = listaVehiculo.FirstOrDefault(vp => vp.Id == personaVehiculo.IdVehiculo);

                    if (persona != null && vehiculo != null)
                    {
                        DatosPersonaVehiculo datosPersonaVehiculo = new DatosPersonaVehiculo
                        {
                            IdPersonaVehiculo = personaVehiculo.Id,
                            Nombre = persona.Nombre,
                            Apellido = persona.Apellido,
                            Marca = vehiculo.Marca,
                            Modelo = vehiculo.Modelo
                        };

                        listaDatosPersonaVehiculo.Add(datosPersonaVehiculo);
                    }
                }

                var PersonaAuto = await _context.PersonaVehiculo
                    .Join(_context.Persona, pV => pV.IdPersona, p => p.Id, (personaVehiculo, persona) => new DatosPersonaVehiculo
                    {
                       // IdPersonaVehiculo = personaVehiculo.IdPersona,
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido,
                    })
                    .ToListAsync();

                response.data = listaDatosPersonaVehiculo;

                var AutoPersona = await _context.PersonaVehiculo
                    .Join(_context.Vehiculo, pV => pV.IdVehiculo, v => v.Id, (personaVehiculo, vehiculo) => new DatosPersonaVehiculo
                    {
                       // IdPersonaVehiculo= personaVehiculo.IdVehiculo,
                        Marca = vehiculo.Marca,
                        Modelo = vehiculo.Modelo,
                    })
                    .ToListAsync();

                response.data = listaDatosPersonaVehiculo;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

                if (idPersona != null)
                {
                    _response.code = 500;
                    _response.message = "ERROR EN ID DE PERSONA. ";
                    _response.error = true;
                    return Ok(_response); //cambiar el return de lugar aqui termina, no debe temranr aqui 
                }
                /*
                 join 
                 */
                if (idVehiculo != null)
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
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarRelacionPersonaVehiculo")]
        public async Task<IActionResult> buscarRelacionPersonaVehiculo(int id)
        {
            PersonaVehiculo buscarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 

            if (buscarRelacion == null)
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
                return Ok(_response);
            }
        }

        [HttpPut]
        [Route("actualizarRelacionPersonaVehiculo")]
        public async Task<IActionResult> actualizarRelacionPersonaVehiculo(PersonaVehiculo personaVehiculo)
        {
            var buscarRelacion = await _context.PersonaVehiculo.FindAsync(personaVehiculo.Id); //consulta que debo agregar al metodo de agregar relacion 
            if (buscarRelacion == null)
            {
                _response.code = 500;
                _response.message = "Relacion no encontrada";
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                buscarRelacion.IdPersona = personaVehiculo.IdPersona;
                buscarRelacion.IdVehiculo = personaVehiculo.IdVehiculo;
               
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Relacion actualizada. ";
                _response.error = false;
                return Ok(_response);
            }
        }

        [HttpDelete]
        [Route("eliminarRelacionPersonaVehiculo")]
        public async Task<IActionResult> eliminarRelacionPersonaVehiculo(int id)
        {
            var eliminarRelacion = await _context.PersonaVehiculo.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            if (eliminarRelacion == null)
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




