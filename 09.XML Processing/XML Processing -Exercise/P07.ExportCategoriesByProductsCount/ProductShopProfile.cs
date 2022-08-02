namespace ProductShop
{

    using System.Linq;

    using ProductShop.Models;
    using ProductShop.Dtos.Export;

    using AutoMapper;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Category, ExportCategoriesByProductsCount>()
                .ForMember(d => d.Name, mo => mo.MapFrom(c => c.Name))
                .ForMember(d => d.Count, mo => mo.MapFrom(c => c.CategoryProducts.Count()))
                .ForMember(d => d.AveragePrice, mo => mo.MapFrom(c => (c.CategoryProducts.Average(p => p.Product.Price))))
                .ForMember(d => d.TotalRevenue, mo => mo.MapFrom(c => (c.CategoryProducts.Sum(p => p.Product.Price))));
        }
    }
}
