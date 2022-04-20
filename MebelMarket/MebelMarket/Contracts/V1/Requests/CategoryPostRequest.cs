using System.ComponentModel.DataAnnotations;

namespace MebelMarket.Contracts.V1.Requests
{
    public class CategoryPostRequest
    {
        [Required] public string Uid { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}
