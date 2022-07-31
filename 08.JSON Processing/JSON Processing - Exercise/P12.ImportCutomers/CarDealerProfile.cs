namespace CarDealer
{
    using CarDealer.Models;
    using CarDealer.DTO.Supplier;
    using CarDealer.DTO.Part;

    using AutoMapper;
    using CarDealer.DTO.Gustomer;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Import Mappers
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();

            this.CreateMap<ImportCustomerDTO, Customer>();

        }
    }
}
