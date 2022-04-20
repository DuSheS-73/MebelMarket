using MebelMarket.Contracts.V1;
using MebelMarket.Contracts.V1.Requests;
using MebelMarket.Contracts.V1.Responses;
using MebelMarket.DAL.EntityModels;
using MebelMarket.DAL.IdentityModels;
using MebelMarket.DAL.Repository.Abstract;
using MebelMarket.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IProductFilesRepository _productFilesRepository;

        public ProductsController(IProductsRepository productsRepository, IProductFilesRepository productFilesRepository)
        {
            _productsRepository = productsRepository;
            _productFilesRepository = productFilesRepository;
        }


        [HttpGet(ApiRoutes.Shared.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Product> products = await _productsRepository.GetAllAsync();

            if (products == null || products.Count() == 0)
                return BadRequest(new DataResponse(null, new[] { "No products found" }));

            return Ok(new DataResponse(products, null));
        }


        [HttpGet(ApiRoutes.Shared.Get)]
        public async Task<IActionResult> GetAsync(string id)
        {
            Product product = await _productsRepository.GetByIdAsync(id);

            if (product == null)
                return BadRequest(new DataResponse(null, new[] { "Product not found" }));

            return Ok(new DataResponse(product, null));
        }


        [CustomAuthorize]
        [HttpPost(ApiRoutes.Shared.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] ProductPostRequest productRequest)
        {
            var user = (ApplicationUser)Request.HttpContext.Items["User"];

            var product = new Product
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Price = productRequest.Price,
                CategoryUid = productRequest.CategoryId,
                UserId = user.Id
            };

            await _productsRepository.InsertAsync(product);

            foreach (var photo in productRequest.Photos)
            {
                //photo.
            }

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
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _productsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
