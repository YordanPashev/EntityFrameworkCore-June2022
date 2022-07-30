namespace ProductShop
{

    using AutoMapper;
    using ProductShop.DTOs.Category;
    using ProductShop.DTOs.CategoryProduct;
    using ProductShop.DTOs.Product;
    using ProductShop.DTOs.User;
    using ProductShop.Models;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Import Mappers
            this.CreateMap<ImportUserDTO, User>();

            this.CreateMap<ImportProductDTO, Product>();

            this.CreateMap<ImportCategoryDTO, Category>();

            this.CreateMap<ImportCategoryProductDTO, CategoryProduct>();


            //Export Mappers
            this.CreateMap<Product, ExportProductInRangeDTO>()
                .ForMember(d => d.SellerFullName, 
                           mo => mo.MapFrom(s => $"{s.Seller.FirstName} {s.Seller.LastName}"));

            this.CreateMap<Product, ExportProductWithBuyerInfoDTO>()
                .ForMember(d => d.BuyerFirstName,
                           mo => mo.MapFrom(p => p.Buyer.FirstName))
                .ForMember(d => d.BuyerLastName,
                           mo => mo.MapFrom(p => p.Buyer.LastName));

            this.CreateMap<Category, ExportCategoriesByProductsCount>()
                .ForMember(d => d.Category, mo => mo.MapFrom(c => c.Name))
                .ForMember(d => d.ProductsCount, mo => mo.MapFrom(c => c.CategoryProducts.Count()))
                .ForMember(d => d.AveragePrice, mo => mo.MapFrom(c => (c.CategoryProducts.Sum(p => p.Product.Price) / c.CategoryProducts.Count()).ToString("F2")))
                .ForMember(d => d.TotalRevenue, mo => mo.MapFrom(c => (c.CategoryProducts.Sum(p => p.Product.Price)).ToString("F2")));

        }
    }
}
