namespace CarDealer
{

    using CarDealer.Dtos.Import;
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
