using MebelMarket.DAL.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MebelMarket.DAL.Repository.Abstract
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(string id);
    }
}
