using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {

            CreateMap<Product, ProductDto>().ForMember(dest => dest.ProductBrand, x => x.MapFrom(p => p.ProductBrand.Name))
                                            .ForMember(dest => dest.ProductType, x => x.MapFrom(p => p.ProductType.Name))
                                            .ForMember(dest => dest.PictureUrl, x => x.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();



        }


    }
}
