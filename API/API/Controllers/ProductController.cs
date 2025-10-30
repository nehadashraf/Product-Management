using API.BL.DTOs.ProductDto;
using API.DAL.Helpers;
using API.BL.Managers.Product;
using API.DAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts(
         [FromQuery] PagingParams pagingParams,
         [FromQuery] string? searchTerm)
        {
           
            var products = await _productManager.GetAllProductsAsync(pagingParams, searchTerm);
            return Ok(products);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
        {
            try
            {
                var project = await _productManager.GetProductByIdAsync(Id);
                return Ok(project);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreationDto productCreationDto)
        {
            try
            {
                var result = await _productManager.CreateProductAsync(productCreationDto);
                if (result == null)
                {
                    return BadRequest("A product with the same name already exists.");
                }

                return Ok(result);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            try
            {
                await _productManager.DeleteProductAsync(Id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }


        [HttpPut("{Id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProject(int Id, ProductCreationDto productUpdateDto)
        {
            try
            {
                var updatedProduct = await _productManager.UpdateProductAsync(Id, productUpdateDto);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }

    }
