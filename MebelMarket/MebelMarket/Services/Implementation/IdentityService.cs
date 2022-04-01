using MebelMarket.DAL.IdentityModels;
using MebelMarket.Models;
using MebelMarket.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MebelMarket.Services.Implementation
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<AuthenticationResult> LoginAsync(string login, string password)
        {
            var user =  await _userManager.FindByNameAsync(login) ??
                        await _userManager.FindByEmailAsync(login);

            if (user == null)
                return new AuthenticationResult(new[] { "Неправильно введен логин или пароль" });

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
                return new AuthenticationResult(new[] { "Неправильно введен логин или пароль" });

            return new AuthenticationResult();
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

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                return new AuthenticationResult(createResult.Errors.Select(x => x.Description));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!addToRoleResult.Succeeded)
                return new AuthenticationResult(addToRoleResult.Errors.Select(x => x.Description));

            return new AuthenticationResult();
        }
    }
}
