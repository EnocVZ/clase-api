using ClaseMiPrimerAPI.DbListContext;
using ClaseMiPrimerAPI.view;
using Microsoft.AspNetCore.Mvc;
using ConcesionariaBarrios.Modelos;
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
            }
        }

    }
}
