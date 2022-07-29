namespace ProductShop
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Data;
    using Models;

    using AutoMapper;
    using Newtonsoft.Json;
    using System;
    using ProductShop.DTOs.Product;
    using ProductShop.DTOs.Category;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            ProductShopContext dbContext = new ProductShopContext();

            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();
            //Console.WriteLine($"Database copy was created!");

            //InitializeOutputFilePath("users-and-products.json");

            InitializeDatasetFilePath("categories.json");
            string inputJson = File.ReadAllText(filePath);
            string addedCategoriesCount = ImportCategories(dbContext, inputJson);
            Console.WriteLine(addedCategoriesCount);
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            ImportCategoryDTO[] cateogryDTO = JsonConvert.DeserializeObject<ImportCategoryDTO[]>(inputJson);

            ICollection<Category> allValidCategories = new List<Category>();

            foreach (ImportCategoryDTO categoryDTO in cateogryDTO)
            {
                if (!IsValid(categoryDTO))
                {
                    continue;
                }

                Category category = Mapper.Map<Category>(categoryDTO);
                allValidCategories.Add(category);
            }

            context.Categories.AddRange(allValidCategories);
            context.SaveChanges();
            return $"Successfully imported {allValidCategories.Count}";
        }


        private static void InitializeDatasetFilePath(string fileName)
        {
            filePath =
                Path.Combine(Directory.GetCurrentDirectory(), "../../../Datasets/", fileName);
        }

        //private static void InitializeOutputFilePath(string fileName)
        //{
        //    filePath =
        //        Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
        //}

        /// <summary>
        /// Executes all validation attributes in a class and returns true or false depending on validation result.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}