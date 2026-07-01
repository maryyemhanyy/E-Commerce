using E_Commerce.API.Specifications;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :base
            (p => (!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
             && (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
