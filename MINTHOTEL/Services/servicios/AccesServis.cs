using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{
	public class AccesServis : IAccess
	{
		private readonly MintHotelsContext _dbContext ;

		public AccesServis(MintHotelsContext dbContext)
		{

			_dbContext = dbContext;
		}

		public async Task<Usuario> GetUsuario(string NomUsuario, string Contraseña)
		{


			Usuario usuario_encontrado = await _dbContext.Usuarios
			 .Include(u => u.IdRolNavigation)
              .Where(u => u.NumUser == NomUsuario && u.Contraseña == Contraseña ).FirstOrDefaultAsync();

			return usuario_encontrado;
		}
	}
}
