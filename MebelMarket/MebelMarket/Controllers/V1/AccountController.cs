using MebelMarket.Contracts.V1;
using MebelMarket.Contracts.V1.Requests;
using MebelMarket.Contracts.V1.Responses;
using MebelMarket.Models;
using MebelMarket.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MebelMarket.Controllers.V1
{
    [ApiController]
    [Route(ApiRoutes.Base)]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiRoutes.Account.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthFailedResponse(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))));

            var authResult = await _identityService.RegisterAsync(request.Email, request.Phone, request.Password, request.Name, request.Surname);
            return GenerateAuthResponse(authResult);
        }


        [HttpPost(ApiRoutes.Account.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var authResult = await _identityService.AuthenticateAsync(request.Login, request.Password);
            return GenerateAuthResponse(authResult);
        }

        private IActionResult GenerateAuthResponse(AuthenticationResult authResult)
        {
            if (!authResult.Success)
                return BadRequest(new AuthFailedResponse(authResult.Errors));

            return Ok(new AuthSuccessResponse(authResult.Token));
        }
    }
}
