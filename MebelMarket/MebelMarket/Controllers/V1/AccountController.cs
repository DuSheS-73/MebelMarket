using MebelMarket.Contracts.V1;
using MebelMarket.Contracts.V1.Requests;
using MebelMarket.Contracts.V1.Responses;
using MebelMarket.Models;
using MebelMarket.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MebelMarket.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route(ApiRoutes.Base)]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Account.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResult = await _identityService.RegisterAsync(request.Email, request.Phone, request.Password, request.Name, request.Surname);
            return GenerateAuthResponse(authResult);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Account.Login)]
        public IActionResult LoginAsync([FromBody] UserLoginRequest loginRequest)
        {
            return View();
        }

        private IActionResult GenerateAuthResponse(AuthenticationResult authResult)
        {
            if (!authResult.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
            });
        }
    }
}
