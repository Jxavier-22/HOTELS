using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Recursos;
using MINTHOTEL.Services.Interfaces;
using System.Security.Claims;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        
        private readonly ISaveUser _usuarioServicio;


        private readonly MintHotelsContext _dbContext;
        public PropietarioController(ISaveUser usuarioServicio, MintHotelsContext dbContext)
        {
            _usuarioServicio = usuarioServicio;
            _dbContext = dbContext;
        }
        public IActionResult UsuarioHotel()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Propietario", "UsuarioHotel");
            }
            else if (!User.IsInRole("Propietario") && !User.IsInRole("Administrator"))
            {
                return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioHotel(Usuario Usuarios, int idHotel, string ConfirmContraseña)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Hay errores en el formulario.", errors });
            }

            if (Usuarios.Contraseña != ConfirmContraseña)
            {
                return Json(new { success = false, message = "Las contraseñas no coinciden." });
            }

            var existingUser = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NumUser == Usuarios.NumUser);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "El nombre de usuario ya está en uso." });
            }
            Usuarios.Contraseña = Utilidades.EncriptarClave(Usuarios.Contraseña);
            Usuarios.IdRol = 4;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int usuarioId))
            {
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

                if (usuario != null)
                {
                    Usuarios.CodHotel = usuario.CodHotel;
                    Usuario usuario_Creado = await _usuarioServicio.SaveUsuario(Usuarios);
                    return Json(new { success = true, message = "Usuario creado" });
                }
            }

            return Json(new { success = false, message = "Error al crear usuario" });
        }
        public async Task<IActionResult> TablasUsuario(string buscar)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (int.TryParse(userId, out int usuarioId))
			{
				var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

				var usuarios = from Usuario in _dbContext.Usuarios
							   where Usuario.CodHotel == usuario.CodHotel && Usuario.IdRol == 4
							   select Usuario;

				if (!String.IsNullOrEmpty(buscar))
				{
					usuarios = usuarios.Where(s => s.NumUser!.Contains(buscar));
				}

				return View(await usuarios.ToListAsync());
			}
            {

            }

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ToggleEstado(Usuario Usuario, int id, bool estado)
		{
			var usuario = await _dbContext.Usuarios.FindAsync(id);
			if (usuario == null)
			{
				return Json(new { success = false, message = "Usuario no encontrado" });
			}

            

            usuario.Estado = estado;
			await _dbContext.SaveChangesAsync();

			return Json(new { success = true });
		}
	}
}
