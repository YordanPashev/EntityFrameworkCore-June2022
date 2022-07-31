namespace CarDealer
{
    using CarDealer.Models;
    using CarDealer.DTO.Supplier;
    using CarDealer.DTO.Part;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Import Mappers
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();
        }
    }
}
