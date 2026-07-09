using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper): IProductService
    {

        public async Task<Result<PaginationResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);

            var repo = unitOfWork.GetRepository<Product, int>();

            var products =await repo.GetAllAsync(spec, ct);

            var data = mapper.Map<IReadOnlyList<ProductDto>>(products);

            var countSpec = new ProductCountSpecification(queryParams);

            var countOfAllProducts = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpec);

            var result = new PaginationResult<ProductDto>(queryParams.PageIndex, queryParams.PageSize, countOfAllProducts, data);

            return Result<PaginationResult<ProductDto>>.Ok(result);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);

            var repo = unitOfWork.GetRepository<Product, int>();
 
            var product = await repo.GetByIdAsync(spec , ct);

            if (product == null) return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product with id:{id}was  not found"));

            return mapper.Map<ProductDto>(product);

        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var repo = unitOfWork.GetRepository<ProductBrand, int>();

            var brands = await repo.GetAllAsync(ct);

            var result = mapper.Map<IReadOnlyList<BrandDto>>(brands);

            return Result<IReadOnlyList<BrandDto>>.Ok(result);

        }



        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();

            var types = await repo.GetAllAsync(ct);

            var result = mapper.Map<IReadOnlyList<TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Ok(result);
        }

       
    }
}
