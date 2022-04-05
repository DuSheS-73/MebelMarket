using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MebelMarket.Contracts.V1.Requests
{
    public class ProductPostRequest
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public string CategoryId { get; set; }

        public IEnumerable<IFormFile> Photos { get; set; }
    }
}
