using MebelMarket.DAL.EntityModels;
using MebelMarket.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MebelMarket.DAL.Repository.Dapper
{
    public class ProductFilesRepository : IProductFilesRepository
    {
        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductFile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductFile> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(ProductFile entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(ProductFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
