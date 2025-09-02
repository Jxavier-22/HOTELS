using System;
using System.Collections.Generic;

namespace MINTHOTEL.Models.Db;

public partial class Usuario
{
	public int IdUsuario { get; set; }

	public string? Nombre { get; set; }

	public string? Apellido { get; set; }

	public string? NumUser { get; set; }

	public string? Contraseña { get; set; }

	public int? CodHotel { get; set; }

	public int? IdRol { get; set; }

	public bool? Estado { get; set; }

	public virtual Hotel? CodHotelNavigation { get; set; }

	public virtual Rol? IdRolNavigation { get; set; }
}
