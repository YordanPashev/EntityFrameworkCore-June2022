namespace CarDealer
{
    using System.IO;
    using System.Xml.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.Dtos.Import;

    using AutoMapper;
    using System;

    public class StartUp
    {
        static string filePath;

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();


            filePath = "../../../Datasets/suppliers.xml";

            string inputXml = File.ReadAllText(filePath);
            string result = ImportSuppliers(context, inputXml);
            Console.WriteLine(result);
        }


        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitialiseMapper();

            string rootElementName = "Suppliers";
            ImportSupplierDTO[] suppliersDTO = XMLConverter.Deserializer<ImportSupplierDTO>(inputXml, rootElementName);
            Supplier[] supplierToAdd = mapper.Map<Supplier[]>(suppliersDTO);

            context.Suppliers.AddRange(supplierToAdd);
            context.SaveChanges();

            int addedSuppliersCount = supplierToAdd.Length;
            return $"Successfully imported {addedSuppliersCount}";
        }


        private static IMapper InitialiseMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
                cfg.AddProfile<CarDealerProfile>());

            return config.CreateMapper();
        }
    }
}
