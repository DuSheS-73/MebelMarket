using System.Collections.Generic;

namespace MebelMarket.Contracts.V1.Responses
{
    public sealed class AuthFailedResponse
    {
        public AuthFailedResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
