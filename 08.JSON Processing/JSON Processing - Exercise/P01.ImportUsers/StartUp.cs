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
    using ProductShop.DTOs;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            //Static because of Judge
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            ProductShopContext dbContext = new ProductShopContext();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            Console.WriteLine($"Database copy was created!");

            //InitializeOutputFilePath("users-and-products.json");

            InitializeDatasetFilePath("users.json");
            string inputJson = File.ReadAllText(filePath);
            string result = ImportUsers(dbContext, inputJson);
            Console.WriteLine(result);
        }

        //Problem 01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            ImportUserDTO[] usersDTO = JsonConvert.DeserializeObject<ImportUserDTO[]>(inputJson);

            ICollection<User> validUsers = new List<User>();

            foreach (ImportUserDTO userDTO in usersDTO)
            {
                if (!IsValid(userDTO))
                {
                    continue;
                }

                User user = Mapper.Map<User>(userDTO);
                validUsers.Add(user);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return $"Successfully imported {validUsers.Count}";
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