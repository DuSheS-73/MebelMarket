using MebelMarket.DAL.EntityModels;
using MebelMarket.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MebelMarket.DAL.Repository.Dapper
{
    public class ProductsRepository : IProductsRepository
    {
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
