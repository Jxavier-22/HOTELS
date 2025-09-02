using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Recursos;
using MINTHOTEL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class CrearUsuariosController : Controller
    {

        private readonly ISaveUser _usuarioServicio;


        private readonly MintHotelsContext _dbContext;
        public CrearUsuariosController(ISaveUser usuarioServicio, MintHotelsContext dbContext)
        {
            _usuarioServicio = usuarioServicio;
            _dbContext = dbContext;
        }
        public IActionResult CrearUsuario(int idHotel)
        {
           
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("CrearUsuario", " CrearUsuarios");
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
        public IActionResult Usuarioadmin()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Usuarioadmin", "CrearUsuarios");
            }
            else if (!User.IsInRole("Administrator"))
            {
                return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario Usuario, int idHotel, string ConfirmContraseña)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Hay errores en el formulario.", errors });
            }

            var existingUser = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NumUser == Usuario.NumUser);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "El nombre de usuario ya está en uso." });
            }


            if (Usuario.Contraseña != ConfirmContraseña)
            {
                return Json(new { success = false, message = "Las contraseñas no coinciden." });
            }

            Usuario.Contraseña = Utilidades.EncriptarClave(Usuario.Contraseña);
            Usuario.Estado = true;
            Usuario.IdRol = 3;
            Usuario.CodHotel = idHotel; // Asociar el usuario al hotel
        

            Usuario usuarioCreado = await _usuarioServicio.SaveUsuario(Usuario);

            if (usuarioCreado.IdUsuario > 0)
            {
                return Json(new { success = true, message = "Usuario registrado correctamente" });
            }

            return Json(new { success = false, message = "Error al crear usuario" });
        }



        [HttpPost]
        public async Task<IActionResult> Usuarioadmin(Usuario Usuario, string ConfirmContraseña)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Hay errores en el formulario.", errors });
            }

            if (Usuario.Contraseña != ConfirmContraseña)
            {
                return Json(new { success = false, message = "Las contraseñas no coinciden." });
            }

            var existingUser = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NumUser == Usuario.NumUser);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "El nombre de usuario ya está en uso." });
            }
            Usuario.Contraseña = Utilidades.EncriptarClave(Usuario.Contraseña);
            Usuario.Estado = true;
            Usuario.CodHotel = null; // No asociar el usuario a un hotel

            Usuario usuario_Creado = await _usuarioServicio.SaveUsuario(Usuario);

            if (usuario_Creado.IdUsuario > 0)
            {
                return Json(new { success = true, message = "Administrador creado correctamente" });
            }

            return Json(new { success = false, message = "Error al crear administrador" });
        }

    }
}
