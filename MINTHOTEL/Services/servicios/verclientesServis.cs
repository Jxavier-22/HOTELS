using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{

    public class verclientesServis : Iverclientes
    {
        private readonly MintHotelsContext _dbContext;

        public verclientesServis(MintHotelsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Visitum>> GetClientesByHotelIdAsync(int hotelId)
        {
            return await _dbContext.Visita
                .Where(c => c.CodHotel == hotelId).ToArrayAsync();
        }
    }





}

