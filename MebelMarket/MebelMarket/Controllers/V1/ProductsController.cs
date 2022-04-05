using MebelMarket.Contracts.V1;
using MebelMarket.Contracts.V1.Requests;
using MebelMarket.Contracts.V1.Responses;
using MebelMarket.DAL.EntityModels;
using MebelMarket.DAL.Repository.Abstract;
using MebelMarket.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MebelMarket.Controllers.V1
{
    [ApiController]
    [Route(ApiRoutes.Base)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }


        [HttpGet(ApiRoutes.Shared.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Product> products = await _productsRepository.GetAllAsync();

            if (products == null || products.Count() == 0)
                return Json(new DataResponse(null, new[] { "No products found" }));

            return Json(new DataResponse(products, null));
        }


        [HttpGet(ApiRoutes.Shared.Get)]
        public async Task<IActionResult> GetAsync(string id)
        {
            Product product = await _productsRepository.GetByIdAsync(id);

            if (product == null)
                return Json(new DataResponse(null, new[] { "Product not found" }));

            return Json(new DataResponse(product, null));
        }


        [CustomAuthorize]
        [HttpPost(ApiRoutes.Shared.Create)]
        public IActionResult CreateAsync([FromBody] ProductPostRequest productRequest)
        {
            return View();
        }


        [CustomAuthorize]
        [HttpPut(ApiRoutes.Shared.Update)]
        public IActionResult UpdateAsync([FromBody] ProductPostRequest productRequest)
        {
            return View();
        }


        [CustomAuthorize]
        [HttpDelete(ApiRoutes.Shared.Delete)]
        public IActionResult DeleteAsync(string id)
        {
            return View();
        }
    }
}
