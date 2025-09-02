using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;
using System.Threading.Tasks;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class vistaHotelController : Controller
    {

        private readonly Iverclientes _verclientes;
        private readonly MintHotelsContext _context;
        public vistaHotelController(Iverclientes verclientes, MintHotelsContext context)
        {

            _verclientes = verclientes;
            _context = context;
        }

        public IActionResult Vistahoteles(string searchString)
        {
            var hoteles = from h in _context.Hotels
                          select h;

            if (!string.IsNullOrEmpty(searchString))
            {
                hoteles = hoteles.Where(h => h.NomHotel.Contains(searchString));
            }

            return View(hoteles.ToList());
        }
        public async Task<IActionResult> clientes(int id, string searchString)
        {
            var clientes = await _verclientes.GetClientesByHotelIdAsync(id);

            if (!string.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(c => c.PrimerNombre.Contains(searchString) || c.PrimerApellido.Contains(searchString)).ToList();
            }

            return View(clientes);

        }
    }
}
