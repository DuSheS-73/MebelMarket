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

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }


        [HttpGet(ApiRoutes.Shared.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Product> products = await _productsRepository.GetAllAsync();

            if (products == null || products.Count() == 0)
                return BadRequest(new DataResponse(null, new[] { "No products found" }));

            return Ok(new DataResponse(products, null));
        }


        [HttpGet(ApiRoutes.Products.GetAllInCategory)]
        public async Task<IActionResult> GetAllAsync(string categoryUid, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = Int32.MaxValue)
        {
            IEnumerable<Product> products = await _productsRepository.GetAllAsync();

            if (products == null || products.Count() == 0)
                return BadRequest(new DataResponse(null, new[] { "No products found" }));

            return Ok(new DataResponse(products, null));
        }


        [HttpGet(ApiRoutes.Shared.Get)]
        public async Task<IActionResult> GetAsync(string uid)
        {
            Product product = await _productsRepository.GetByIdAsync(uid);

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
                CategoryUid = productRequest.CategoryUid,
                UserId = user.Id
            };

            await _productsRepository.InsertAsync(product);

            return View();
        }


        [CustomAuthorize]
        [HttpPut(ApiRoutes.Shared.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductPostRequest productRequest)
        {
            var product = new Product
            {
                Uid = productRequest.Uid,
                Name = productRequest.Name,
                Description = productRequest.Description,
                Price = productRequest.Price,
                CategoryUid = productRequest.CategoryUid,
            };

            int updateResult = await _productsRepository.UpdateAsync(product);

            if (updateResult == 0)
                return BadRequest(new DataResponse(null, new[] { "Error while updating the product" }));

            return Ok();
        }


        [CustomAuthorize]
        [HttpDelete(ApiRoutes.Shared.Delete)]
        public async Task<IActionResult> DeleteAsync(string uid)
        {
            int deleteResult = await _productsRepository.DeleteAsync(uid);

            if (deleteResult == 0)
                return BadRequest(new DataResponse(null, new[] { "Error while deleting the product" }));

            return Ok();
        }
    }
}
