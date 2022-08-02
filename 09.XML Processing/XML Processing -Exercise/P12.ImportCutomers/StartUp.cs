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

            string inputXml = File.ReadAllText("../../../Datasets/customers.xml");
            string result = ImportCustomers(context, inputXml);
            Console.WriteLine(result);
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitialiseMapper();

            string rootElementName = "Customers";
            ImportCustomerDTO[] customersDTO = XMLConverter.Deserializer<ImportCustomerDTO>(inputXml, rootElementName);

            Customer[] cutomers = mapper.Map <Customer[]>(customersDTO);

            context.Customers.AddRange(cutomers);
            context.SaveChanges();

            int addedCustomersCount = customersDTO.Count();
            return $"Successfully imported {addedCustomersCount}";
        }

        private static IMapper InitialiseMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
                cfg.AddProfile<CarDealerProfile>());

            return config.CreateMapper();
        }
    }
}