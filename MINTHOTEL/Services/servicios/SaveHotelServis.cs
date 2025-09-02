using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{
    public class SaveHotelServis : ISaveHotel
    {

        private readonly MintHotelsContext _dbContext;
        public SaveHotelServis(MintHotelsContext bdContext)
        {
            _dbContext = bdContext;
        }

        public async Task<Hotel> SaveHotel(Hotel hotel)
        {
            _dbContext.Hotels.Add(hotel);
            await _dbContext.SaveChangesAsync();
            return hotel;
        }
        public async Task<bool> IsCodRucExists(string codRuc)
        {
            return await _dbContext.Hotels.AnyAsync(h => h.CodRuc == codRuc);
        }
    }
}
