using MagicVilla.MagicApi.Model;

namespace MagicVilla.MagicApi.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {

        Task<VillaNumber> UpdateAsync(VillaNumber entity);

    }
}
