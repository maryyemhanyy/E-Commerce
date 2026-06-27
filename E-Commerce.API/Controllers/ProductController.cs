using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) :APIBaseController
    {
        #region Get All Products
        [HttpGet]
        [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>>GetAllProducts(CancellationToken ct)
        {
            var products = await productService.GetAllProductsAsync(ct);
            return ToActionResult(products);
        }

        #endregion

        #region Get Product

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>>GetProductById(int id , CancellationToken ct)
        {
            var product = await productService.GetProductByIdAsync(id, ct);
            return ToActionResult(product);
        }

        #endregion

        #region Gat All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>>GetAllBrands(CancellationToken ct)
        {
            var brands =await productService.GetAllBrandsAsync(ct);
            return ToActionResult(brands);
        }
        #endregion

        #region Gat All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var types = await productService.GetAllTypesAsync(ct);
            return ToActionResult(types);
        }
        #endregion
    }
}
