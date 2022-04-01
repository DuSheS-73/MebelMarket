using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MebelMarket.Contracts.V1.Responses
{
    public sealed class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
