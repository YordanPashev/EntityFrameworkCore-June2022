namespace ProductShop
{

    using AutoMapper;
    using ProductShop.DTOs;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDTO, User>();
        }
    }
}
