using MINTHOTEL.Models.Db;

namespace MINTHOTEL.Services.Interfaces
{
    public interface Iverclientes
    {
        Task<IEnumerable<Visitum>> GetClientesByHotelIdAsync(int hotelId);
    }
}
