namespace CarDealer
{
    using System.IO;
    using System.Linq;
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


            filePath = "../../../Datasets/parts.xml";

            string inputXml = File.ReadAllText(filePath);
            string result = ImportParts(context, inputXml);
            Console.WriteLine(result);
        }


        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitialiseMapper();

            string rootElementName = "Parts";
            ImportPartDTO[] partsDTo = XMLConverter.Deserializer<ImportPartDTO>(inputXml, rootElementName);
            Part[] partsToAdd = mapper.Map<Part[]>(partsDTo.Where(p => context.Suppliers
                                                                              .Any(s =>s.Id == p.SupplierId)));

            context.Parts.AddRange(partsToAdd);
            context.SaveChanges();

            int addedPartsCount = partsToAdd.Length;
            return $"Successfully imported {addedPartsCount}";
        }


        private static IMapper InitialiseMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
                cfg.AddProfile<CarDealerProfile>());

            return config.CreateMapper();
        }
    }
}
