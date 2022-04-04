using MebelMarket.DAL.IdentityModels;
using MebelMarket.Models;
using MebelMarket.Models.Configuration;
using MebelMarket.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MebelMarket.Services.Implementation
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string login, string password)
        {
            var user =  await _userManager.FindByNameAsync(login) ??
                        await _userManager.FindByEmailAsync(login);

            if (user == null)
                return new AuthenticationResult(new[] { "Неправильно введен логин или пароль" });

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
                return new AuthenticationResult(new[] { "Неправильно введен логин или пароль" });

            string token = GenerateJwtToken(user);

            return new AuthenticationResult(token);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string phone, string password, string name, string surname)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return new AuthenticationResult(new[] { "Пользователь с таким E-mail уже существует" });

            existingUser = await _userManager.FindByNameAsync(phone);

            if (existingUser != null)
                return new AuthenticationResult(new[] { "Нельзя использовать данный номер телефона" });

            var user = new ApplicationUser
            {
                Email = email,
                PhoneNumber = phone,
                UserName = phone,
                Name = name,
                Surname = surname,
            };

            IdentityResult createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                return new AuthenticationResult(createResult.Errors.Select(x => x.Description));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!addToRoleResult.Succeeded)
                return new AuthenticationResult(addToRoleResult.Errors.Select(x => x.Description));

            string token = GenerateJwtToken(user);

            return new AuthenticationResult(token);
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
