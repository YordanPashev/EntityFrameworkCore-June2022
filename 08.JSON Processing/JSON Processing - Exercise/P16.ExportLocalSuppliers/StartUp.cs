namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using CarDealer.Data;
    using CarDealer.DTO.Supplier;

    using AutoMapper;
    using Newtonsoft.Json;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/local-suppliers.json");


            string jsonResult = GetLocalSuppliers(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            ExportSupplierDTO[] localSupplierNotImportingParts = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportSupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToArray();

            string jsonResult = JsonConvert.SerializeObject(localSupplierNotImportingParts, Formatting.Indented);

            return jsonResult;
        }
    }
}