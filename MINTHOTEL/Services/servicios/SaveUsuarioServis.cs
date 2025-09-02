using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{
    public class SaveUsuarioServis:ISaveUser
    {
        private readonly MintHotelsContext _dbContext;
        public SaveUsuarioServis(MintHotelsContext bdContext)
        {
            _dbContext = bdContext;
        }

        public async Task<Usuario> SaveUsuario(Usuario Usuarios)
        {
            _dbContext.Usuarios.Add(Usuarios);
            await _dbContext.SaveChangesAsync();
            return Usuarios;
        }
    }
}
