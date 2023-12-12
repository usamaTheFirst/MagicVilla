using MagicVilla.MagicApi.Model;
using System.Linq.Expressions;

namespace MagicVilla.MagicApi.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa entity);



    }
}
