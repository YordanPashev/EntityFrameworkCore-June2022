namespace ProductShop
{
    using System.IO;
    using System.Linq;

    using Data;
    using Models;

    using AutoMapper;
    using Newtonsoft.Json;
    using System;
    using ProductShop.DTOs.Product;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            ProductShopContext dbContext = new ProductShopContext();

            InitializeOutputFilePath("products-in-range.json");

            string json = GetProductsInRange(dbContext);

            File.WriteAllText(filePath, json);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            ExportProductDTO[] listOfPoductsInRage = context.Products
                                                .Where(p => p.Price >= 500 && p.Price <= 1000)
                                                .OrderBy(p => p.Price)
                                                .ProjectTo<ExportProductDTO>()
                                                .ToArray();

            string json = JsonConvert.SerializeObject(listOfPoductsInRage, Formatting.Indented);
            return json;
        }

        private static void InitializeOutputFilePath(string fileName)
            => filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
    }
}