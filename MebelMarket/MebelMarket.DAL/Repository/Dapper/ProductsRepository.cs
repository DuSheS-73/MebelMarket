using Dapper;
using MebelMarket.DAL.EntityModels;
using MebelMarket.DAL.Helpers;
using MebelMarket.DAL.Repository.Abstract;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MebelMarket.DAL.Repository.Dapper
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ConnectionHelper _connectionHelper;

        public ProductsRepository(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> DeleteAsync(string uid)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@uid", uid);

                var sql = "DELETE FROM [dbo].[Products] WHERE [uid] = @uid";

                return await cnn.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "SELECT * FROM [dbo].[Products] ORDER BY [CreationDate] DESC";

                return await cnn.QueryAsync<Product>(sql, commandType: CommandType.Text);
            }
        }

        public Task<IEnumerable<Product>> GetAllInCategoryAsync(string categoryUid, int currentPage = 1, int pageSize = int.MaxValue)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(string uid)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@uid", uid);

                var sql = "SELECT * FROM [dbo].[Products] WHERE [uid] = @uid";

                return await cnn.QueryFirstOrDefaultAsync<Product>(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task<int> InsertAsync(Product entity)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "INSERT INTO [dbo].[Products] VALUES (NEWID(), @Name, @Description, @Price, @CategoryUid, @UserId, GETUTCDATE())";

                return await cnn.ExecuteAsync(sql, entity, commandType: CommandType.Text);
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "UPDATE [dbo].[Products] SET [Name] = @Name, [Description] = @Description, [Price] = @Price, [CategoryUid] = @CategoryUid WHERE [uid] = @Uid";

                return await cnn.ExecuteAsync(sql, entity, commandType: CommandType.Text);
            }
        }

        //private async Task<IEnumerable<Article>> QueryWithMappingAsync(IDbConnection cnn, string sql, DynamicParameters parameters)
        //{
        //    var articleDictionary = new Dictionary<string, Article>();

        //    return (await cnn.QueryAsync<Article, Category, Article>(
        //                sql,
        //                (article, category) =>
        //                {
        //                    if (!articleDictionary.TryGetValue(article.uid, out var articleEntry))
        //                    {
        //                        articleEntry = article;
        //                        articleEntry.Categories = new List<Category>();
        //                        articleDictionary.Add(article.uid, articleEntry);
        //                    }

        //                    articleEntry.Categories = articleEntry.Categories.Append(category);
        //                    return articleEntry;
        //                },
        //                parameters,
        //                splitOn: "uid"
        //                ))
        //                .Distinct();
        //}
    }
}
