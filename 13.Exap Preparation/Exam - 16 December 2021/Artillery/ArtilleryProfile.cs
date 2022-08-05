namespace Artillery
{
    using Artillery.Data.Models;
    using Artillery.DataProcessor.ImportDto;

    using AutoMapper;
    class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            //Auto Mappers for impor procedures
            CreateMap<ImportCountryDto, Country>();
            CreateMap<ImportManufactorerDto, Manufacturer>();
            CreateMap<ImportShellDto, Shell>();
        }
    }
}