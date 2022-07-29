namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Models;
    using FastFood.Services.Models.Categories;
    using FastFood.Services.Models.Positions;
    using FastFood.Web.ViewModels.Categories;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<CreatePositionDTO, Position>();

            this.CreateMap<CreatePositionInputModel, CreatePositionDTO>()
                .ForMember(d => d.Name, mo => mo.MapFrom(src => src.PositionName));

            this.CreateMap<Position, ListPositionDTOs>();

            this.CreateMap<ListPositionDTOs, PositionsAllViewModel>();

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Category
            this.CreateMap<CreateCategoryDTO, Category>();

            this.CreateMap<CreateCategoryInputModel, CreateCategoryDTO>()
                .ForMember(d => d.Name, mo => mo.MapFrom(src => src.CategoryName));

            this.CreateMap<Category, ListCategoryDTOs>();

            this.CreateMap<ListCategoryDTOs, CategoryAllViewModel>();
        }
    }
}
