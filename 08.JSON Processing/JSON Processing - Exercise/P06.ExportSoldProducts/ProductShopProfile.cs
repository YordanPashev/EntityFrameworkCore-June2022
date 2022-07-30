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

        }
    }
}
