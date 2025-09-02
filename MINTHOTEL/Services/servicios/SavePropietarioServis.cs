using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;

namespace MINTHOTEL.Services.servicios
{
    public class SavePropietarioServis: ISavePropietario
    {

        private readonly MintHotelsContext _dbContext;
        public SavePropietarioServis(MintHotelsContext bdContext)
        {
            _dbContext = bdContext;
        }

        public async Task<Propietario> SavePrpietario(Propietario propietario)
        {
            _dbContext.Propietarios.Add(propietario);
            await _dbContext.SaveChangesAsync();
            return propietario;
        }
    }
}
