using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;

namespace ClaseMiPrimerAPI.Controllers
{
    [Route("vehiculoControllerBarrios/[controller]")]
    [ApiController]
    public class VehiculoController
    {
        private readonly ILogger<VehiculoController> logger;
        public VehiculoController(ILogger<VehiculoController> paramLogger)
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

    }
}
