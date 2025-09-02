using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using System.Security.Claims;

namespace MINTHOTEL.Controllers
{
    [Authorize]
    public class RegistrosController : Controller
    {
        private readonly MintHotelsContext _dbContext;

        public RegistrosController(MintHotelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> TablasVisita(string buscar, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Convertir el ID del usuario a entero (si es necesario)
            if (int.TryParse(userId, out int usuarioId))
            {
                // Consultar la base de datos para obtener el usuario actual
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

                // Consulta que incluye los datos del hotel del usuario y los visitantes
                var visitas = from visita in _dbContext.Visita
                              join hotel in _dbContext.Hotels on visita.CodHotel equals hotel.IdHotel
                              //where visita.CodHotel == usuario.CodHotel
                              select new
                              {
                                  IdVisita = visita.IdVisita,
                                  PrimerNombre = visita.PrimerNombre,
                                  SegundoNombre = visita.SegundoNombre,
                                  PrimerApellido = visita.PrimerApellido,
                                  SegundoApellido = visita.SegundoApellido,
                                  FechaNac = visita.FechaNac,
                                  Sexo = visita.Sexo,
                                  Nacionalidad = visita.Nacionalidad,
                                  TipoIdentificion = visita.TipoIdentificion,
                                  NumIdentificion = visita.NumIdentificion,
                                  NumHabitacion = visita.NumHabitacion,
                                  FechaIngreso = visita.FechaIngreso,
                                  FechaSalida = visita.FechaSalida,
                                  CodHotel = visita.CodHotel,
                                  HotelNombre = hotel.NomHotel  // Seleccionar solo el nombre del hotel
                              };

                // Filtrar por el nombre del visitante y fecha de ingreso si se proporciona un término de búsqueda o fechas
                if (!String.IsNullOrEmpty(buscar) || fechaInicio.HasValue || fechaFin.HasValue)
                {
                    visitas = visitas.Where(v => 
                        (String.IsNullOrEmpty(buscar) || v.PrimerNombre.Contains(buscar)) &&
                        (!fechaInicio.HasValue || v.FechaIngreso >= fechaInicio.Value) &&
                        (!fechaFin.HasValue || v.FechaIngreso <= fechaFin.Value));
                }

                // Proyectar los resultados en una lista
                var visitumList = await visitas.ToListAsync();

                return View(visitumList);
            }

            return View();
        }

        public async Task<IActionResult> registroHoteles(string buscar)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Convertir el ID del usuario a entero (si es necesario)
            if (int.TryParse(userId, out int usuarioId))
            {
                // Consultar la base de datos para obtener el usuario actual
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

                // Consulta que incluye los datos del hotel del usuario y los visitantes
                var Hoteles = from Hotel in _dbContext.Hotels
                              join Propietario in _dbContext.Propietarios on Hotel.IdHotel equals Propietario.CodHotl
                              //join user in _dbContext.Usuarios on Hotel.IdHotel equals user.CodHotel
                              select new
                              {
                                  NomHotel = Hotel.NomHotel,
                                  Departamento = Hotel.Departamento,
                                  Municipio = Hotel.Municipio,
                                  CodRuc = Hotel.CodRuc,
                                  Tipo = Hotel.Tipo,
                                  PrimerNom = Propietario.PrimerNom,
                                  PrimerApellido = Propietario.PrimerApellido,
                                  NumIdentificion = Propietario.NumIdentificion,
                                  //NumUser = user.NumUser,
                                  //Contraseña = user.Contraseña,
                              };

                // Filtrar por el nombre del hotel si se proporciona un término de búsqueda
                if (!String.IsNullOrEmpty(buscar))
                {
                    Hoteles = Hoteles.Where(v => v.NomHotel.Contains(buscar));
                }

                // Proyectar los resultados en una lista
                var HotelsList = await Hoteles.ToListAsync();

                return View(HotelsList);
            }

            return View();
        }
    }
}