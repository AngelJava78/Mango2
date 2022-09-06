using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto response;
        readonly IProductRepository repository;

        public ProductAPIController(IProductRepository repository)
        {
            this.repository = repository;
            response = new ResponseDto
            {
                IsSuccess = false,
                ErrorMessages = new List<string> (),
                Result = null,
                DisplayMessage = string.Empty
            };
        }
        
        [HttpGet]
        [Authorize]
        public async Task<object> Get()
        {
            try
            {
                var products = await repository.GetProducts();
                response.IsSuccess = true;
                response.Result = products;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Result = null;
                response.DisplayMessage = ex.Message;
                
            }
            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                var product = await repository.GetProductById(id);
                response.IsSuccess = true;
                response.Result = product;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Result = null;
                response.DisplayMessage = ex.Message;

            }
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var model = await repository.CreateUpdateProduct(productDto);
                response.IsSuccess = true;
                response.Result = model;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Result = null;
                response.DisplayMessage = ex.Message;

            }
            return response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var model = await repository.CreateUpdateProduct(productDto);
                response.IsSuccess = true;
                response.Result = model;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Result = null;
                response.DisplayMessage = ex.Message;

            }
            return response;
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var isSuccess = await repository.DeleteProduct(id);
                response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
                response.Result = null;
                response.DisplayMessage = ex.Message;

            }
            return response;
        }

    }
}
