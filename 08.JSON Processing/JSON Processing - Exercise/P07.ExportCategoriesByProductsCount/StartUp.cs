namespace ProductShop
{
    using System.IO;
    using System.Linq;

    using Data;

    using AutoMapper;
    using Newtonsoft.Json;

    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using ProductShop.DTOs.Category;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            ProductShopContext dbContext = new ProductShopContext();

            InitializeOutputFilePath("categories-by-products.json");

            string json = GetCategoriesByProductsCount(dbContext);

            File.WriteAllText(filePath, json);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            ExportCategoriesByProductsCount[] listOfBuyerWithSoldProducts = context.Categories
                                               .Include(cp => cp.CategoryProducts)
                                               .ThenInclude(p => p.Product)
                                               .OrderByDescending(c => c.CategoryProducts.Count())
                                               .ProjectTo<ExportCategoriesByProductsCount>()
                                               .ToArray();

            string json = JsonConvert.SerializeObject(listOfBuyerWithSoldProducts, Formatting.Indented);
            return json;
        }

        private static void InitializeOutputFilePath(string fileName)
            => filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
    }
}