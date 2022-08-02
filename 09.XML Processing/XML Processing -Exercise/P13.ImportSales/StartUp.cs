namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    
    using AutoMapper;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();

            string inputXml = File.ReadAllText("../../../Datasets/sales.xml");
            string result = ImportSales(context, inputXml);
            Console.WriteLine(result);
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitialiseMapper();

            string rootElementName = "Sales";
            ImportSaleDTO[] salesDTO = XMLConverter.Deserializer<ImportSaleDTO>(inputXml, rootElementName);

            Sale[] sales = mapper.Map <Sale[]>(salesDTO.Where(s => context.Cars.Any(c => c.Id == s.CarId)));

            context.Sales.AddRange(sales);
            context.SaveChanges();

            int addedSalesCount = sales.Count();

            return $"Successfully imported {addedSalesCount}";
        }

        private static IMapper InitialiseMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
                cfg.AddProfile<CarDealerProfile>());

            return config.CreateMapper();
        }
    }
}