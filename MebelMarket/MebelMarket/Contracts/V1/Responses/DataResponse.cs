using System.Collections.Generic;

namespace MebelMarket.Contracts.V1.Responses
{
    public class DataResponse
    {
        public DataResponse(dynamic data, IEnumerable<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public dynamic Data { get; }
        public IEnumerable<string> Errors { get; }
    }
}
