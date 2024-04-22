using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using ConcesionariaBarrios.Modelos;
using ClaseMiPrimerAPI.Model; 
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly BaseDatosContext _context;
        private readonly ResponseVenta _response; 

        public VentaController(BaseDatosContext context)
        {
            _context = context;
        }
        
        //LISTAR RELACIONES 
        [HttpGet]
        [Route("listaVentas")]
        public async Task<ActionResult<Venta>> listaVentas()
        {
            try
            {
                var listaVentas = await _context.Venta.ToListAsync();
                var listaPersona = await _context.Persona.ToListAsync();
                var listaVehiculo = await _context.Vehiculo.ToListAsync();
                var listaConcesionario = await _context.Concesionario.ToListAsync();
                var listaVendedor = await _context.Vendedor.ToListAsync();
                List<DatosVenta> ListaVentasRealizadas = new List<DatosVenta>(); 
                for(var i = 0; i < listaVentas.Count; i++)
                {
                    var ventasList = listaVentas[i];
                    var persona = listaPersona.Where(p => p.Id == ventasList.IdPersona).FirstOrDefault();
                    var vehiculo = listaVehiculo.Where(v => v.Id == ventasList.IdVehiculo).FirstOrDefault();
                    var concesionario = listaConcesionario.Where(c => c.Id == ventasList.IdConcesionario).FirstOrDefault();
                    var vendedor = listaVendedor.Where(vn => vn.Id == ventasList.IdVendedor).FirstOrDefault(); 
                    //SI NO ESTAN VACIOS, CREAR MI VENTA 
                    if(persona != null && vehiculo !=null && concesionario !=null && vendedor !=null)
                    {
                        DatosVenta ventaNueva = new DatosVenta
                        {
                            IdVenta = ventasList.Id, 
                            NombrePersona = persona.Nombre, 
                            ApellidoPersona = persona.Apellido, 
                            NombreVendedor = vendedor.Nombre, 
                            ApellidoVendedor = vendedor.Apellido, 
                            NombreConcesionario = concesionario.Nombre, 
                            DireccionConcesionario = concesionario.Direccion, 
                            Marca = vehiculo.Marca, 
                            Modelo = vehiculo.Modelo, 
                            Total = ventasList.Total
                        };
                        ListaVentasRealizadas.Add(ventaNueva); 
                    }
                }


            }catch(Exception ex) { return Ok(ex.Message); }
        }

        [HttpPost]
        [Route("agregarVenta")]
        public async Task<ActionResult<ResponseVenta>> agregarVenta(RequestVenta requestVenta)
        {
            try
            {
                var idPersona = await _context.Persona.FindAsync(requestVenta.IdPersona);
                var idVehiculo = await _context.Vehiculo.FindAsync(requestVenta.IdVehiculo);
                var idVendedor = await _context.Vendedor.FindAsync(requestVenta.IdVendedor);
                var idConcesionaria = await _context.Concesionario.FindAsync(requestVenta.IdConcesionario);

                if(idPersona != null)
                {
                    _response.error = true;
                    _response.message = "Persona no encontrada. ";
                    _response.code = 500; 
                }
                if (idVehiculo != null)
                {
                    _response.error = true;
                    _response.message = "Vehiculo no encontrado. ";
                    _response.code = 500;
                }
                if (idVendedor != null)
                {
                    _response.error = true;
                    _response.message = "Vendedor no encontrado. ";
                    _response.code = 500;
                }
                if (idConcesionaria != null)
                {
                    _response.error = true;
                    _response.message = "Consesionaria no encontrada. ";
                    _response.code = 500;
                }
                else
                {
                    Venta agregarVenta = new Venta
                    {
                        IdPersona = requestVenta.IdPersona,
                        IdVehiculo = requestVenta.IdVehiculo, 
                        IdConcesionario = requestVenta.IdConcesionario,
                        IdVendedor = requestVenta.IdVendedor, 
                        Fecha = requestVenta.Fecha, 
                        Total = requestVenta.Total
                    };
                    await _context.Venta.AddAsync(agregarVenta);
                    await _context.SaveChangesAsync();
                    _response.code = 200;
                    _response.message = "Venta agregada";
                    _response.error = false;
                    _response.Venta = agregarVenta;
                }
                return Ok(_response);
            }
            catch (Exception ez) 
            { 
                return Ok(ez.Message); 
            }
        }

        [HttpGet]
        [Route("buscarVenta")]
        public async Task<IActionResult> buscarVenta(int id)
        {
            Venta buscarVenta = await _context.Venta.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 

            if (buscarVenta == null)
            {
                _response.code = 500;
                _response.message = "Venta no encontrada";
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                _response.code = 200;
                _response.message = "Venta encontrada";
                _response.error = false;
                _response.Venta = buscarVenta;
                return Ok(_response);
            }
        }

        [HttpPut]
        [Route("actualizarVenta")]
        public async Task<IActionResult> actualizarVenta(Venta venta)
        {
            var buscarVenta = await _context.Venta.FindAsync(venta.Id); //consulta que debo agregar al metodo de agregar relacion 
            if (buscarVenta == null)
            {
                _response.code = 500;
                _response.message = "Venta no encontrada";
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                buscarVenta.IdPersona = venta.IdPersona;
                buscarVenta.IdVehiculo = venta.IdVehiculo;
                buscarVenta.IdVendedor = venta.IdVendedor;
                buscarVenta.IdConcesionario = venta.IdConcesionario; 
                buscarVenta.Fecha = venta.Fecha;
                buscarVenta.Total = venta.Total;
                await _context.SaveChangesAsync();

                _response.code = 200;
                _response.message = "Venta actualizada. ";
                _response.error = false;
                return Ok(_response);
            }
        }

        [HttpDelete]
        [Route("eliminarVenta")]
        public async Task<IActionResult> eliminarVenta(int id)
        {
            var eliminarVenta = await _context.Venta.FindAsync(id); //consulta que debo agregar al metodo de agregar relacion 
            if (eliminarVenta == null)
            {
                _response.code = 500;
                _response.message = "Venta no encontrada";
                _response.error = true;
                return Ok(_response);
            }
            else
            {
                /*            vehiculoContext.Vehiculo.Remove(vehiculoEliminar);
            await vehiculoContext.SaveChangesAsync(); */
                _context.Venta.Remove(eliminarVenta);
                await _context.SaveChangesAsync();
                _response.code = 200;
                _response.message = "Relacion eliminada";
                _response.error = false;
                _response.VentaEncontrada = eliminarVenta;
                return Ok(_response);
            }
        }

    }
}
