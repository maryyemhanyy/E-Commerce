using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");

            builder.Property(p => p.Description).HasColumnType("nvarchar(500)");

            builder.Property(p => p.PictureUrl).HasColumnType("nvarchar(200)");

            builder.Property(p => p.Price).HasColumnType("decimal(18, 2)");

            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.TypeId);



        }
    }
}
