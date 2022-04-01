using Dapper;
using MebelMarket.DAL.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MebelMarket.DAL.IdentityModels
{
    public sealed class RoleStore : IRoleStore<ApplicationRole>, IDisposable
    {
        private readonly ConnectionHelper _connectionHelper;

        public RoleStore(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"INSERT INTO [AspNetRoles](Id,Name,NormalizedName) VALUES (@id,@name,@normalizedName)";

                var param = new DynamicParameters();
                param.Add("@id", role.Id);
                param.Add("@name", role.Name);
                param.Add("@normalizedName", role.NormalizedName);

                var result = await conn.ExecuteAsync(query, param: param, commandType: CommandType.Text);

                if (result > 0)
                    return IdentityResult.Success;
                else
                    return IdentityResult.Failed(new IdentityError() { Code = "120", Description = "Cannot Create Role!" });
            }
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"DELETE FROM [AspNetRoles] WHERE id=@id";

                var param = new DynamicParameters();
                param.Add("@id", role.Id);

                var result = await conn.ExecuteAsync(query, param: param, commandType: CommandType.Text);

                if (result > 0)
                    return IdentityResult.Success;
                else
                    return IdentityResult.Failed(new IdentityError() { Code = "120", Description = "Cannot Delete Role!" });
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"SELECT * FROM [AspNetRoles] WHERE id=@id";

                var param = new DynamicParameters();
                param.Add("@id", roleId);

                return await conn.QueryFirstOrDefaultAsync<ApplicationRole>(query, param: param, commandType: CommandType.Text);
            }
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"SELECT * FROM [AspNetRoles] WHERE NormalizedName=@normalizedName";

                var param = new DynamicParameters();
                param.Add("@normalizedName", normalizedRoleName);

                return await conn.QueryFirstOrDefaultAsync<ApplicationRole>(query, param: param, commandType: CommandType.Text);
            }
        }

        public async Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return await Task.FromResult(role.NormalizedName);
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return await Task.FromResult(role.Id);
        }

        public async Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return await Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await Task.FromResult(role.NormalizedName = normalizedName);
        }

        public async Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await Task.FromResult(role.Name = roleName);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }
    }
}
