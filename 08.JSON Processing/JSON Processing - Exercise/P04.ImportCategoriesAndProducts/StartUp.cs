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
    using ProductShop.DTOs.CategoryProduct;

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

            InitializeDatasetFilePath("categories-products.json");
            string inputJson = File.ReadAllText(filePath);
            string addedCategoriesProductsCountResult = ImportCategoryProducts(dbContext, inputJson);
            Console.WriteLine(addedCategoriesProductsCountResult);
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            ImportCategoryProductDTO[] cateogryProductsDTO = JsonConvert.DeserializeObject<ImportCategoryProductDTO[]>(inputJson);

            ICollection<CategoryProduct> allValidCategoriesProducts = new List<CategoryProduct>();

            foreach (ImportCategoryProductDTO categoryProductDTO in cateogryProductsDTO)
            {
                //if (context.Categories.Any(c => c.Id == categoryProductDTO.CategoryId) &&
                //    context.Products.Any(p => p.Id == categoryProductDTO.ProductId))
                //{
                //    CategoryProduct category = Mapper.Map<CategoryProduct>(categoryProductDTO);
                //    allValidCategoriesProducts.Add(category);
                //}

                CategoryProduct category = Mapper.Map<CategoryProduct>(categoryProductDTO);
                allValidCategoriesProducts.Add(category);   
            }

            context.CategoryProducts.AddRange(allValidCategoriesProducts);
            context.SaveChanges();
            return $"Successfully imported {allValidCategoriesProducts.Count}";
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