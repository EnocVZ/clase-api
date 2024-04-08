using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("apiList/[controller]")]
    [ApiController]
    public class VehiculoListController : ControllerBase
    {
        private readonly ILogger<VehiculoController> logger;

        public VehiculoListController(ILogger<VehiculoController> paramLogger)
        {
            this.logger = paramLogger; //se usa para el registro de la informacion?
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
        [HttpPost("guardarVehiculoList")]
        public ResponseVehiculo GuardarVehiculo(Vehiculo vehiculo)
        {
            List<Vehiculo> listaPostVehiculo = this.ListaVehiculosAlmacenados();
            listaPostVehiculo.Add(vehiculo);

            ResponseVehiculo responsePostVehiculo = new ResponseVehiculo();

            responsePostVehiculo.Vehiculo = vehiculo;
            responsePostVehiculo.ListaVehiculos = listaPostVehiculo;
            return responsePostVehiculo;
        }

        [HttpGet("buscarVehiculoList")]
        public ResponseVehiculo buscarVehiculo(int id)
        {
            ResponseVehiculo responseGetVehiculo = new ResponseVehiculo();
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

        [HttpPut("actualizarVehiculoList")]
        public ResponseVehiculo actualizarVehiculo(Vehiculo vehiculo)
        {
            ResponseVehiculo responsePutVehiculo = new ResponseVehiculo();
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
            responsePutVehiculo.ListaVehiculos = listaVehiculosAlmacenados;

            return responsePutVehiculo;
        }

        [HttpDelete("eliminarVehiculoList")]
        public ResponseVehiculo eliminarVehiculo(int id)
        {
            ResponseVehiculo responsePostVehiculo = new ResponseVehiculo();
            List<Vehiculo> listaVehiculosAlmacenados = this.ListaVehiculosAlmacenados();

            for (int i = 0; i < listaVehiculosAlmacenados.Count; i++)
            {
                if (listaVehiculosAlmacenados[i].Id == id)
                {
                    listaVehiculosAlmacenados.Remove(listaVehiculosAlmacenados.ElementAt(i));
                }
            }
            responsePostVehiculo.ListaVehiculos = listaVehiculosAlmacenados;
            return responsePostVehiculo;
        }
        //GUARDAR EN CODIGO ###########################################################

    }
}
