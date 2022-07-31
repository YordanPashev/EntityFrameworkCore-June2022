namespace CarDealer
{
    using CarDealer.Models;
    using CarDealer.DTO.Supplier;
    using CarDealer.DTO.Part;

    using AutoMapper;
    using CarDealer.DTO.Gustomer;
    using CarDealer.DTO.Sales;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Auto Mappers for Import procedures
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();

            this.CreateMap<ImportCustomerDTO, Customer>();

            this.CreateMap<ImportSaleDTO, Sale>();

            //Auto Mapper for Export procedures

            this.CreateMap<Customer, ExportCustomerDTO>();
        }
    }
}
