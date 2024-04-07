using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClaseMiPrimerAPI.Controllers
{
    [Route("apiVehiculoBarrios/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;
        private readonly VehiculoContext vehiculoContext;
        public VehiculoController(ILogger<VehiculoController> paramLogger, VehiculoContext vehiculoContext)
        {
            this.logger = paramLogger; //se usa para el registro de la informacion?
            this.vehiculoContext = vehiculoContext; 
        }
        //lista de vehiculos
        [HttpGet("listaVehiculosAlmacenados")]
        public List<Vehiculo> ListaVehiculosAlmacenados()
        {
            List<Vehiculo> listaVehiculosAlmacenados = new List<Vehiculo>();

            for (int i = 0; i <= 10; i++)
            {
                Vehiculo vehiculo = new Vehiculo
                {
                    Id = i,
                    Marca = "Marca " + i,
                    Modelo = "Modelo " + i,
                    Color = "Color " + i,
                    Anio = "200" + i
                };
                listaVehiculosAlmacenados.Add(vehiculo);
            }

            return listaVehiculosAlmacenados;
        }

        //guardar vehiculos
        [HttpPost("guardarVehiculo")]
        public PostVehiculo GuardarVehiculo(Vehiculo vehiculo)
        {
            List<Vehiculo> listaPostVehiculo = this.ListaVehiculosAlmacenados();
            listaPostVehiculo.Add(vehiculo);

            PostVehiculo responsePostVehiculo = new PostVehiculo();

            responsePostVehiculo.Vehiculo = vehiculo;
            responsePostVehiculo.ListaVehiculo = listaPostVehiculo;
            return responsePostVehiculo;
        }

        [HttpGet("buscarVehiculo")]
        public GetVehiculo buscarVehiculo(int id)
        {
            GetVehiculo responseGetVehiculo = new GetVehiculo();
            List<Vehiculo> ListaVehiculosAlmacenados = this.ListaVehiculosAlmacenados();
            Vehiculo buscarVehiculo = new Vehiculo();

            for (int i = 0; i < ListaVehiculosAlmacenados.Count; i++)
            {
                if (ListaVehiculosAlmacenados[i].Id == id)
                {
                    buscarVehiculo = ListaVehiculosAlmacenados[i];
                }
            }
            responseGetVehiculo.vehiculoEncontrado = buscarVehiculo;
            return responseGetVehiculo;

        }

        [HttpPut("actualizarVehiculo")]
        public PutVehiculo actualizarVehiculo(Vehiculo vehiculo)
        {
            PutVehiculo responsePutVehiculo = new PutVehiculo();
            List<Vehiculo> listaVehiculosAlmacenados = this.ListaVehiculosAlmacenados();
            Vehiculo vehiculoModificado = new Vehiculo();

            for (int i = 0; i < listaVehiculosAlmacenados.Count; i++)
            {
                if (listaVehiculosAlmacenados[i].Id == vehiculo.Id)
                {
                    vehiculoModificado = listaVehiculosAlmacenados[i];
                    listaVehiculosAlmacenados[i].Marca = vehiculo.Marca;
                    listaVehiculosAlmacenados[i].Modelo = vehiculo.Modelo;
                    listaVehiculosAlmacenados[i].Color = vehiculo.Color;
                    listaVehiculosAlmacenados[i].Anio = vehiculo.Anio;
                }
            }
            responsePutVehiculo.message = vehiculoModificado.Marca; 
            responsePutVehiculo.ListaVehiculo = listaVehiculosAlmacenados;

            return responsePutVehiculo;
        }

        [HttpDelete("eliminarVehiculo")]
        public PostVehiculo eliminarVehiculo(int id)
        {
            PostVehiculo responsePostVehiculo = new PostVehiculo();
            List<Vehiculo> listaVehiculosAlmacenados = this.ListaVehiculosAlmacenados();

            for (int i = 0; i < listaVehiculosAlmacenados.Count; i++)
            {
                if (listaVehiculosAlmacenados[i].Id == id)
                {
                    listaVehiculosAlmacenados.Remove(listaVehiculosAlmacenados.ElementAt(i));
                }
            }
            responsePostVehiculo.ListaVehiculo = listaVehiculosAlmacenados;
            return responsePostVehiculo;
        }
        //GUARDAR EN CODIGO ###########################################################

        //GUARDAR EN BASE DE DATOS ###########################################################
        
        [HttpGet]
        [Route("mostrarVehiculos")]
        public async Task<List<Vehiculo>> mostrarVehiculos()
        {
            return await vehiculoContext.Vehiculo.ToListAsync(); 
        }

        [HttpPost]
        [Route("crearVehiculo")]
        public async Task<ActionResult<Response>> crearVehiculo(RequestVehiculo vehiculo)
        {
            try
            {
                Response responseVehiculo = new Response();
                Vehiculo agregarVehiculo = new Vehiculo
                {
                    Marca = vehiculo.Marca, 
                    Modelo = vehiculo.Modelo,
                    Color = vehiculo.Color, 
                    Anio = vehiculo.Anio
                };
                await vehiculoContext.Vehiculo.AddAsync(agregarVehiculo);
                await vehiculoContext.SaveChangesAsync();
                responseVehiculo.code = 200;
                responseVehiculo.message = "El vehiculo se guardo correctamente. ";
                responseVehiculo.error = false;
                return Ok(responseVehiculo); 
            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }
}
