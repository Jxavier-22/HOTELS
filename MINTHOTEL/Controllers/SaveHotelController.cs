using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;
using MINTHOTEL.Services.servicios;

namespace MINTHOTEL.Controllers
{
    
    [Authorize]
    public class SaveHotelController : Controller
    {

        private readonly ISaveHotel _Savehotelservis;

        public SaveHotelController(ISaveHotel savehotelservis)
        {
            _Savehotelservis = savehotelservis;
        }
        public IActionResult RegistrarHotel()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("RegistrarHotel", "SaveHotel");
            }
            else if (!User.IsInRole("Administrador") && !User.IsInRole("Administrator"))
            {
                return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarHotel(Hotel hotel)
        {
            if (await _Savehotelservis.IsCodRucExists(hotel.CodRuc))
            {
                TempData["ErrorMessage"] = "El código RUC ya está en uso.";
                return View("RegistrarHotel", hotel);
            }

            Hotel Hotel_Registrado = await _Savehotelservis.SaveHotel(hotel);
            if (Hotel_Registrado.IdHotel > 0)
            {
                TempData["SuccessMessage"] = "Hotel registrado exitosamente";
                return RedirectToAction("ProRegistro", "SavePropietario", new { idHotel = Hotel_Registrado.IdHotel });
            }

            return View(hotel);
        }
    }
}
