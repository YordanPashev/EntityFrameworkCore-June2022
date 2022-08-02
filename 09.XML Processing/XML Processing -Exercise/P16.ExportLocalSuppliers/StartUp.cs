namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string filePath = "../../../Results/local-suppliers.xml";
            string xmlStringResult = GetLocalSuppliers(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetLocalSuppliers(CarDealerContext context) 
        { 

            string rootElementName = "suppliers";

            ExportLocalSupplierDTO[] carsWithDrivers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportLocalSupplierDTO
                {
                    Id = s.Id,
                    name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            string xmlStringResult = XMLConverter.Serialize(carsWithDrivers, rootElementName);

            return xmlStringResult;
        }
    }
}