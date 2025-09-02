using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class SavePropietarioController : Controller
    {

        private readonly ISavePropietario _PropietarioServicio;
        private readonly MintHotelsContext _dbContext;



        public SavePropietarioController(ISavePropietario ProServicio, MintHotelsContext dbContext)
        {
            _PropietarioServicio = ProServicio;
            _dbContext = dbContext;
        }
        public IActionResult ProRegistro(int idHotel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ProRegistro", "SavePropietario");
            }
            else if (!User.IsInRole("Administrador") && !User.IsInRole("Administrator"))
            {
                return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
            }
            else
            {
                int ultimoIdHotel = _dbContext.Hotels.OrderByDescending(h => h.IdHotel).Select(h => h.IdHotel).FirstOrDefault();
                ViewBag.IdHotel = ultimoIdHotel;
                return View();
            }

            
        }
        [HttpPost]

        public async Task<IActionResult> ProRegistro(Propietario propietario, int idHotel)
        {

            propietario.CodHotl = idHotel;  // Asociar el propietario al hotel registrado
            Propietario Pro_registro = await _PropietarioServicio.SavePrpietario(propietario);
            if (Pro_registro.IdPropietario > 0)
            {
                ViewBag.Mensaje = "Propietario registrado correctamente";
                return RedirectToAction("CrearUsuario", "CrearUsuarios");
            }
            return View();
        }
    }
}
