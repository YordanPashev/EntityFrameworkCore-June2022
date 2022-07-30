namespace ProductShop
{

    using AutoMapper;
    using ProductShop.DTOs.Category;
    using ProductShop.DTOs.CategoryProduct;
    using ProductShop.DTOs.Product;
    using ProductShop.DTOs.User;
    using ProductShop.Models;

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
            this.CreateMap<Product, ExportProductDTO>()
                .ForMember(d => d.SellerFullName, mo => mo.MapFrom(s => $"{s.Seller.FirstName} {s.Seller.LastName}"));

        }
    }
}
