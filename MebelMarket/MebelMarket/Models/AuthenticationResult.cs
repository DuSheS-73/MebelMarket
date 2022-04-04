using System.Collections.Generic;

namespace MebelMarket.Models
{
    public sealed class AuthenticationResult
    {
        public AuthenticationResult(string token)
        {
            Token = token;
            Success = true;
        }

        public AuthenticationResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public string Token { get; }
        public bool Success { get; }
        public IEnumerable<string> Errors { get; }
    }
}
