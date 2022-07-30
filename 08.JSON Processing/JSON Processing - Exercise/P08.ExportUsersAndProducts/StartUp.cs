namespace ProductShop
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Data;
    using DTOs.Category;
    using DTOs.CategoryProduct;
    using DTOs.Product;
    using DTOs.User;
    using Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            ProductShopContext dbContext = new ProductShopContext();
            InitializeOutputFilePath("users-and-products.json");

            //InitializeDatasetFilePath("categories.json");
            //string inputJson = File.ReadAllText(filePath);

            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();

            //Console.WriteLine($"Database copy was created!");

            string json = GetUsersWithProducts(dbContext);
            File.WriteAllText(filePath, json);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ExportProductNameAndPriceDTO>();

                cfg.CreateMap<User, ExportProductCountAndListOfProductsDTO>()
                    .ForMember(d => d.ProductsSold, mo => mo.MapFrom(ps => ps.ProductsSold.Where(p => p.BuyerId.HasValue)));

                cfg.CreateMap<User, ExportUserProductsDTO>()
                    .ForMember(d => d.ProductsSold, mo => mo.MapFrom(s => s));
            });

            ExportUserProductsDTO[] usersProductsInfo = context.Users
                                                        .Include(p => p.ProductsSold)
                                                        .Where(p => p.ProductsSold.Any(ps => ps.BuyerId.HasValue))
                                                        .OrderByDescending(p => p.ProductsSold.Count(p => p.BuyerId.HasValue))
                                                        .ProjectTo<ExportUserProductsDTO>()
                                                        .ToArray();

            UserCountAndAllProductsInfoDTO usersInfo = new UserCountAndAllProductsInfoDTO(usersProductsInfo);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(usersInfo, Formatting.Indented, serializerSettings);

            return json;
        }

        private static void InitializeOutputFilePath(string fileName)
        {
            filePath =
                Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
        }
    }
}