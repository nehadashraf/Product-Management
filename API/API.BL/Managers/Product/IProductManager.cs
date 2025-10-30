using API.BL.DTOs.ProductDto;
using API.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BL.Managers.Product
{
    public interface IProductManager
    {
        Task<PaginationResult<ProductDto>>GetAllProductsAsync(PagingParams PagingParams,string? searchTerm);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateProductAsync(ProductCreationDto productCreationDto);
        Task<ProductDto> UpdateProductAsync(int productId, ProductCreationDto productUpdateDto);
        Task DeleteProductAsync(int productId);
    }
}
