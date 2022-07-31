namespace CarDealer
{

    using System.IO;
    using System.Linq;

    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.DTO.Part;

    using AutoMapper;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filepath = ("../../../Datasets/parts.json"); 
            string json = File.ReadAllText(filepath);

            string result = ImportParts(context, json);
            System.Console.WriteLine(result);
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            ImportPartDTO[] partsDTO = JsonConvert.DeserializeObject<ImportPartDTO[]>(inputJson);
            Part[] parts = Mapper.Map<Part[]>(partsDTO.Where(s => IsSupplierIdExistInDb(context, s.SupplierId)));

            context.Parts.AddRange(parts);
            context.SaveChanges();

            int addingPatsCount = parts.Length;
            return $"Successfully imported {addingPatsCount}.";
        }

        public static bool IsSupplierIdExistInDb(CarDealerContext context, int supplierID)
            => context.Suppliers.Any(s => s.Id == supplierID);
    }
}