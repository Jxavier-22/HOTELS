 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;
using System.ComponentModel;
using System.Security.Claims;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class SaveVisitaController : Controller
    {
        private readonly MintHotelsContext _dbContext;
        private readonly IVista _cliente;

        public SaveVisitaController( MintHotelsContext dbContext, IVista VisitaServis  )
        {
           
            _dbContext = dbContext;
            _cliente= VisitaServis;
        }
        public IActionResult SaveVisitas()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SaveVisita", "SaveVisitas");
            }
            else if (!User.IsInRole("Usuario") && !User.IsInRole("Administrator"))
            {
                return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveVisitas(Visitum cliente)
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            if (int.TryParse(userId, out int usuarioId))
            {
               
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

                if (usuario != null)
                {
                    // Asignar el valor de CodHotel del usuario a la visita
                    cliente.CodHotel = usuario.CodHotel;
                    // Guardar la visita en la base de datos
                    Visitum huespedRegistrado = await _cliente.SaveVisita(cliente);
                    // Aquí puedes realizar otras operaciones si es necesario
                    return RedirectToAction("loby","Home");
                }

			}
			return View();
		}
        //public IActionResult TablasVisita()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("SaveVisita", "TablasVisita");
        //    }
        //    else if (!User.IsInRole("Usuario"))
        //    {
        //        return Forbid(); // Retorna un código de estado 403 Forbidden si el usuario no tiene el rol necesario
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        //[HttpPost]
        public async Task<IActionResult> TablasVisita(string buscar)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Convertir el ID del usuario a entero (si es necesario)
            if (int.TryParse(userId, out int usuarioId))
            {
                // Consultar la base de datos para obtener el usuario actual
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

        
                    var Visitas = from Visitum in _dbContext.Visita
                                  where Visitum.CodHotel == usuario.CodHotel && Visitum.FechaSalida == null
                                  select Visitum;
                 
                if (!String.IsNullOrEmpty(buscar))
                {
                    Visitas = Visitas.Where(s => s.PrimerNombre!.Contains(buscar));
                }

                return View(await Visitas.ToListAsync());

            }
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarSalida(int idVisita)
        {
            var visita = await _dbContext.Visita.FindAsync(idVisita);
            if (visita != null)
            {
                visita.FechaSalida = DateTime.Now;
                _dbContext.Visita.Update(visita);
                await _dbContext.SaveChangesAsync();



            }
            return RedirectToAction("TablasVisita");
        }

    }
}
