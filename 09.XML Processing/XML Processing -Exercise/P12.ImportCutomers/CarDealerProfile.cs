namespace CarDealer
{

    using CarDealer.Models;
    using CarDealer.Dtos.Import;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportCustomerDTO, Customer>();
        }
    }
}
