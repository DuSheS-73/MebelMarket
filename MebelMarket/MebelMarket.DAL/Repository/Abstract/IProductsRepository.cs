using MebelMarket.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MebelMarket.DAL.Repository.Abstract
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllInCategoryAsync(string categoryUid, int currentPage = 1, int pageSize = Int32.MaxValue);
    }
}
