using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;
using System.Security.Claims;
using System.ComponentModel;
using System.Threading.Tasks;
using MINTHOTEL.Recursos;

namespace MINTHOTEL.Controllers
{
    public class InicioController : Controller
    {
        private readonly IAccess _AccesServi;
        private readonly MintHotelsContext _dbContext;

        public InicioController(IAccess AccesServisio, MintHotelsContext DbContext)
        {
            _AccesServi = AccesServisio;
            _dbContext = DbContext;
        }

        public async Task<IActionResult> Login()
        {
            // Cierra la sesión actual si la hay
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string NomUsuario, string Contraseña)
        {
            try
            {
                Usuario usuario_encontrado = await _AccesServi.GetUsuario(NomUsuario, Utilidades.EncriptarClave(Contraseña));
                if (usuario_encontrado == null)
                {
                    ViewBag.mensaje = "Usuario y/o contraseña incorrectos";
                    return View();
                }

                // Verificar si el usuario está activo
                if (usuario_encontrado.Estado != true) // Esta línea verifica el estado
                {
                    ViewBag.mensaje = "El usuario está deshabilitado, no puedes iniciar sesión";
                    return View();
                }

                // Cargar la propiedad de navegación IdRolNavigation
                await _dbContext.Entry(usuario_encontrado).Reference(u => u.IdRolNavigation).LoadAsync();

                if (usuario_encontrado.IdRolNavigation != null)
                {
                    // Crear una lista de claims para el usuario
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.IdUsuario.ToString()), // Usar el ID del usuario como identificador
                        new Claim(ClaimTypes.Role, usuario_encontrado.IdRolNavigation.NomRol), // Agregar el rol como claim
                        new Claim(ClaimTypes.Name, $"{usuario_encontrado.Nombre} {usuario_encontrado.Apellido}")
                    };

                    // Crear la identidad del usuario con los claims
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var properties = new AuthenticationProperties
                    {
                        AllowRefresh = true
                    };

                    // Iniciar sesión del usuario
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("loby", "Home");
                }
                else
                {
                    // Manejar el caso en el que el usuario no tiene un rol asignado
                    ViewBag.mensaje = "El usuario no tiene un rol asignado";
                    return View();
                }
            }
            catch (Win32Exception ex)
            {
                // Loguear el error si es necesario
                // Redirigir a una página de error amigable
                return RedirectToAction("Erroracceso", new { message = "Ocurrió un problema al procesar tu solicitud. Por favor, inténtalo de nuevo más tarde." });
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción
                // Loguear el error si es necesario
                return RedirectToAction("Erroracceso", new { message = "Tenemos problemas para acceder a este sitio. Por favor, inténtalo de nuevo más tarde." });
            }
        }

        public IActionResult Erroracceso(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Inicio");
        }
    }
}