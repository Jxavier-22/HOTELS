using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{
    public class VisitaServis:IVista
    {
        private readonly MintHotelsContext _dbContext;
        public VisitaServis(MintHotelsContext bdContext)
        {
            _dbContext = bdContext;
        }

        public async Task<Visitum> SaveVisita(Visitum visita)
        {
            _dbContext.Visita.Add(visita);
            await _dbContext.SaveChangesAsync();
            return visita;
        }
    }
}
