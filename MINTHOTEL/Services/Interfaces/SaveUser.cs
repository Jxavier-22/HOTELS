using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
    public interface ISaveUser
    {
        Task<Usuario> SaveUsuario(Usuario Usuarios);
    }
}
