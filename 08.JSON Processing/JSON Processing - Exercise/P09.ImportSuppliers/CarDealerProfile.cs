namespace CarDealer
{

    using CarDealer.DTO.Supplier;
    using CarDealer.Models;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDTO, Supplier>();
        }
    }
}
