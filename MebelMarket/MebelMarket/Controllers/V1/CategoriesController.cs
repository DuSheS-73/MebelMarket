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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }


        [HttpGet(ApiRoutes.Shared.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Category> products = await _categoriesRepository.GetAllAsync();

            if (products == null || products.Count() == 0)
                return BadRequest(new DataResponse(null, new[] { "No products found" }));

            return Ok(new DataResponse(products, null));
        }

        [CustomAuthorize]
        [HttpPost(ApiRoutes.Shared.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryPostRequest categoryRequest)
        {
            var category = new Category
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description
            };

            int insertResult = await _categoriesRepository.InsertAsync(category);

            if (insertResult == 0)
                return BadRequest(new DataResponse(null, new[] { "Error while inserting new category" }));
            
            return Ok();
        }


        [CustomAuthorize]
        [HttpPut(ApiRoutes.Shared.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryPostRequest categoryRequest)
        {
            var category = new Category
            {
                Uid = categoryRequest.Uid,
                Name = categoryRequest.Name,
                Description = categoryRequest.Description
            };

            int updateResult = await _categoriesRepository.UpdateAsync(category);

            if (updateResult == 0)
                return BadRequest(new DataResponse(null, new[] { "Error while updating the category" }));

            return Ok();
        }


        [CustomAuthorize]
        [HttpDelete(ApiRoutes.Shared.Delete)]
        public async Task<IActionResult> DeleteAsync(string uid)
        {
            int deleteResult = await _categoriesRepository.DeleteAsync(uid);

            if (deleteResult == 0)
                return BadRequest(new DataResponse(null, new[] { "Error while deleting the category" }));

            return Ok();
        }
    }
}
