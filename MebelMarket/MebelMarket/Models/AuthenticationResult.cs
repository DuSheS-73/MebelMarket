using System.Collections.Generic;

namespace MebelMarket.Models
{
    public sealed class AuthenticationResult
    {
        public AuthenticationResult()
        {
            Success = true;
        }

        public AuthenticationResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public bool Success { get; }
        public IEnumerable<string> Errors { get; }
    }
}
