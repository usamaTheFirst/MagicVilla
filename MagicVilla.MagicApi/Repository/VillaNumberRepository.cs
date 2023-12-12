using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Repository.IRepository;

namespace MagicVilla.MagicApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        internal readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
