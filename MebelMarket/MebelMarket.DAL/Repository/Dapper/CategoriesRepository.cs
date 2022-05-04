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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ConnectionHelper _connectionHelper;

        public CategoriesRepository(ConnectionHelper connectionHelper)
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "SELECT * FROM [dbo].[Products] ORDER BY [CreationDate] DESC";

                return await cnn.QueryAsync<Category>(sql, commandType: CommandType.Text);
            }
        }

        public async Task<Category> GetByIdAsync(string uid)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@uid", uid);

                var sql = "SELECT * FROM [dbo].[Products] WHERE [uid] = @uid";

                return await cnn.QueryFirstOrDefaultAsync<Category>(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task<int> InsertAsync(Category entity)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "INSERT INTO [dbo].[Products] VALUES (NEWID(), @Name, @Description, @Price, @CategoryUid, @UserId, GETUTCDATE())";

                return await cnn.ExecuteAsync(sql, entity, commandType: CommandType.Text);
            }
        }

        public async Task<int> UpdateAsync(Category entity)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var sql = "UPDATE [dbo].[Products] SET [Name] = @Name, [Description] = @Description, [Price] = @Price, [CategoryUid] = @CategoryUid WHERE [uid] = @Uid";

                return await cnn.ExecuteAsync(sql, entity, commandType: CommandType.Text);
            }
        }
    }
}
