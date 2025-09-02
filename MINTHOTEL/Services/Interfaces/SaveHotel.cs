using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
    public interface ISaveHotel
    {
        Task<Hotel>SaveHotel(Hotel hotel);
        Task<bool> IsCodRucExists(string codRuc);
    }
}
