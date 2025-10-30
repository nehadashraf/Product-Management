using API.BL.DTOs.ProductDto;
using API.DAL.Helpers;
using API.DAL.Models;
using API.DAL.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BL.Managers.Product
{
    public class ProductManger: IProductManager
    {
        private readonly IProductRepo _productRepo;
        public ProductManger(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(PagingParams pagingParams, string? searchTerm = null)
        {
            var pagedProducts = await _productRepo.GetAllAsync(pagingParams);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                pagedProducts.Items = pagedProducts.Items
                    .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var dtoList = pagedProducts.Items.Select(p => new ProductDto
            {
                Id= p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();

            return new PaginationResult<ProductDto>
            {
                Items = dtoList,
                Metadata = new PaginationMetadata
                {
                    CurrentPage = pagedProducts.Metadata.CurrentPage,
                    PageSize = pagedProducts.Metadata.PageSize,
                    TotalCount = pagedProducts.Metadata.TotalCount,
                    TotalPages = pagedProducts.Metadata.TotalPages
                }
            };
        }


        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product =  await _productRepo.GetByIdAsync(productId);
            if (product == null)
            {
                return null;
            }
            return new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreationDto productCreationDto)
        {
            var existingProduct = await _productRepo.FindProductByName(productCreationDto.Name);
            if (existingProduct != null)
                return null;
            if (productCreationDto.Price <= 0)
                throw new InvalidDataException("Price must be more than 0");

            var newProduct = new API.DAL.Models.Product
            {
                Sku = $"PROD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}",
                Name = productCreationDto.Name,
                Description = productCreationDto.Description,
                Price = productCreationDto.Price,
                CreatedAtUtc = DateTime.UtcNow,
            };
             await _productRepo.AddAsync(newProduct);
            await _productRepo.SaveChangesAsync();
            return new ProductDto
            {
                Id = newProduct.Id,
                Sku = newProduct.Sku,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
            };
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Project not found");
            }
            product.IsDeleted = true;
            await _productRepo.UpdateAsync(product);
            await _productRepo.SaveChangesAsync();

        }


        public async Task<ProductDto> UpdateProductAsync(int productId, ProductCreationDto productUpdateDto)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null || product.IsDeleted)
            {
                throw new KeyNotFoundException("Product not found or has been deleted");
            }
            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;
            product.UpdatedAtUtc = DateTime.UtcNow;

            await _productRepo.UpdateAsync(product);  
            await _productRepo.SaveChangesAsync();

            return new ProductDto
            {
                Id=product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}
