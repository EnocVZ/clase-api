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
    }
}
