using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
	public interface IAccess
	{
		Task<Usuario> GetUsuario(string NomUsuario, string Contraseña);
	}
}
