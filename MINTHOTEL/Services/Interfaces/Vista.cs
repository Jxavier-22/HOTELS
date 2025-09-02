using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
    public interface IVista
    {

        Task<Visitum> SaveVisita(Visitum cliente);
    }
}
