using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla.MagicApi.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
             _db.Update(entity);
           await  _db.SaveChangesAsync();
            return entity;
        }
    }
}
