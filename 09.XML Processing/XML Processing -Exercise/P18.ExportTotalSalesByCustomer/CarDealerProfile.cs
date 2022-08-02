namespace CarDealer
{

    using System.Linq;

    using CarDealer.Models;
    using CarDealer.Dtos.Import;
    using CarDealer.Dtos.Export;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSaleDTO, Sale>();


            CreateMap<Customer, ExportCustomerSalesDTO>()
                .ForMember(d => d.FullName, mo => mo.MapFrom(c => c.Name))
                .ForMember(d => d.CarsBought,
                    mo => mo.MapFrom(
                        s => s.Sales.Count))
                .ForMember(d => d.MoneySpent,
                    mo => mo.MapFrom(
                        s => s.Sales
                            .Select(s => s.Car)
                            .SelectMany(c => c.PartCars)
                            .Sum(pt => pt.Part.Price)));
        }
    }
}
