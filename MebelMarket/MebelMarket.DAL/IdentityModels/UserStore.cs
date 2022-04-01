using Dapper;
using MebelMarket.DAL.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MebelMarket.DAL.IdentityModels
{
    public sealed class UserStore :
        IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>,
        IDisposable
    {
        private readonly ConnectionHelper _connectionHelper;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserStore(ConnectionHelper connectionHelper, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _connectionHelper = connectionHelper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"INSERT INTO [AspNetUsers](" +
                    $"[Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed]," +
                    $"[PasswordHash],[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled],[AccessFailedCount]" +
                    $")" +
                    $"VALUES(@Id,@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,@SecurityStamp,@ConcurrencyStamp,@PhoneNumber,@PhoneNumberConfirmed," +
                    $"@TwoFactorEnabled,@LockoutEnd,@LockoutEnabled,@AccessFailedCount)";
                var param = new DynamicParameters();
                param.Add("@Id", user.Id);
                param.Add("@UserName", user.UserName);
                param.Add("@NormalizedUserName", user.NormalizedUserName);
                param.Add("@Email", user.Email);
                param.Add("@NormalizedEmail", string.IsNullOrEmpty(user.Email) ? null : user.Email.ToUpper());
                param.Add("@EmailConfirmed", user.EmailConfirmed);
                param.Add("@PasswordHash", user.PasswordHash);
                param.Add("@SecurityStamp", user.SecurityStamp);
                param.Add("@ConcurrencyStamp", user.ConcurrencyStamp);
                param.Add("@PhoneNumber", user.PhoneNumber);
                param.Add("@PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                param.Add("@TwoFactorEnabled", user.TwoFactorEnabled);
                param.Add("@LockoutEnd", user.LockoutEnd);
                param.Add("@LockoutEnabled", user.LockoutEnabled);
                param.Add("@AccessFailedCount", user.AccessFailedCount);
                var result = await conn.ExecuteAsync(query, param: param, commandType: CommandType.Text);

                if (result > 0)
                    return IdentityResult.Success;
                else
                    return IdentityResult.Failed(new IdentityError() { Code = "120", Description = "Cannot Create User!" });
            }
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"DELETE FROM [AspNetUsers] WHERE [Id] = @Id";
                var param = new DynamicParameters();
                param.Add("@Id", user.Id);

                var result = await conn.ExecuteAsync(query, param: param, commandType: CommandType.Text);

                if (result > 0)
                    return IdentityResult.Success;
                else
                    return IdentityResult.Failed(new IdentityError() { Code = "120", Description = "Cannot Update User!" });
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"SELECT * FROM [AspNetUsers] WHERE [NormalizedEmail] = @NormalizedEmail";
                var param = new DynamicParameters();
                param.Add("@NormalizedEmail", normalizedEmail);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(query, param: param, commandType: CommandType.Text);
            }
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"SELECT * FROM [AspNetUsers] WHERE [Id] = @Id";
                var param = new DynamicParameters();
                param.Add("@Id", userId);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(query, param: param, commandType: CommandType.Text);
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"SELECT * FROM [AspNetUsers] WHERE [NormalizedUserName] = @normalizedUserName";
                var param = new DynamicParameters();
                param.Add("@normalizedUserName", normalizedUserName);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(query, param: param, commandType: CommandType.Text);
            }
        }

        public async Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.Email);
        }

        public async Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.EmailConfirmed);
        }

        public async Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.NormalizedEmail);
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.NormalizedUserName);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.Run(() => user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Email = email;
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.EmailConfirmed = confirmed;
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var query = $"UPDATE [AspNetUsers]" +
                    $"SET" +
                    $"[PasswordHash] = @PasswordHash," +
                    $"[SecurityStamp] = @SecurityStamp," +
                    $"[ConcurrencyStamp] = @ConcurrencyStamp," +
                    $"[TwoFactorEnabled] = @TwoFactorEnabled," +
                    $"[LockoutEnd] = @LockoutEnd," +
                    $"[LockoutEnabled] = @LockoutEnabled," +
                    $"[AccessFailedCount] = @AccessFailedCount " +
                    $"WHERE [Id] = @Id";
                var param = new DynamicParameters();
                param.Add("@Id", user.Id);
                param.Add("@PasswordHash", user.PasswordHash);
                param.Add("@SecurityStamp", user.SecurityStamp);
                param.Add("@ConcurrencyStamp", user.ConcurrencyStamp);
                param.Add("@PhoneNumber", user.PhoneNumber);
                param.Add("@PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                param.Add("@TwoFactorEnabled", user.TwoFactorEnabled);
                param.Add("@LockoutEnd", user.LockoutEnd);
                param.Add("@LockoutEnabled", user.LockoutEnabled);
                param.Add("@AccessFailedCount", user.AccessFailedCount);

                var result = await conn.ExecuteAsync(query, param: param, commandType: CommandType.Text);

                if (result > 0)
                    return IdentityResult.Success;
                else
                    return IdentityResult.Failed(new IdentityError() { Code = "120", Description = "Cannot Update User!" });
            }
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);
                param.Add("@RoleName", roleName);

                var sql = "dbo.AddUserToRole";

                await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var param = new DynamicParameters();
                param.Add("@RoleName", roleName);

                var sql = "dbo.GetUsersInRole";

                return (await conn.QueryAsync<ApplicationUser>(sql, param, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);

                var sql = "dbo.GetUserRoles";

                return (await conn.QueryAsync<string>(sql, param, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);
                param.Add("@RoleName", roleName);

                var sql = "dbo.IsUserInRole";

                return await conn.QueryFirstAsync<bool>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            using (IDbConnection conn = new SqlConnection(_connectionHelper.CnnVal))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);
                param.Add("@RoleName", roleName);

                var sql = "dbo.RemoveUserFromRole";

                await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
