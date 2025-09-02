using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
    public interface ISavePropietario
    {
        Task<Propietario>SavePrpietario(Propietario propietario);
    }
}
