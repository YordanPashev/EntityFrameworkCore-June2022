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
    using Microsoft.EntityFrameworkCore;
    using ProductShop.DTOs.User;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            ProductShopContext dbContext = new ProductShopContext();

            InitializeOutputFilePath("users-sold-products.json");

            string json = GetSoldProducts(dbContext);

            File.WriteAllText(filePath, json);
        }

        public static string GetSoldProducts(ProductShopContext context) 
        {
            ExportUserSoldProductsDTO[] listOfBuyerWithSoldProducts = context.Users
                                               .Include(p => p.ProductsSold)
                                               .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                                               .OrderBy(s => s.LastName)
                                               .ThenBy(s => s.FirstName)
                                               .ProjectTo<ExportUserSoldProductsDTO>()
                                               .ToArray();

            string json = JsonConvert.SerializeObject(listOfBuyerWithSoldProducts, Formatting.Indented);
            return json;
        }

        private static void InitializeOutputFilePath(string fileName)
            => filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
    }
}