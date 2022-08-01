namespace ProductShop
{
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using XmlFacade;
    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.Dtos.Import;

    using AutoMapper;
    using System.Collections.Generic;
    using System;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            InitializeFilePath();

            string inputXml = File.ReadAllText(filePath);
            string result = ImportCategoryProducts(context, inputXml);
            System.Console.WriteLine(result);
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            string rootElementName = "CategoryProducts";

            ImportCategoryProductDTO[] extracedCategoryProductsFromXml = XMLConverter.Deserializer<ImportCategoryProductDTO>(inputXml, rootElementName);

            List<CategoryProduct> validCategoryProducts = new List<CategoryProduct>();

            foreach (ImportCategoryProductDTO categoryProductDTO in extracedCategoryProductsFromXml)
            {
                
                if (!AreCategoryAndProductExist(context, categoryProductDTO))
                {
                    continue;
                }

                CategoryProduct validCategoryProduct = new CategoryProduct()
                {
                    CategoryId = categoryProductDTO.CategoryId,
                    ProductId = categoryProductDTO.ProductId
                };

                validCategoryProducts.Add(validCategoryProduct);
            }

            context.CategoryProducts.AddRange(validCategoryProducts);
            context.SaveChanges();

            int addedUsersCount = validCategoryProducts.Count;
            return $"Successfully imported {addedUsersCount}";
        }

        private static bool AreCategoryAndProductExist(ProductShopContext context, ImportCategoryProductDTO categoryProductDTO)
        {
            if (!context.Products.Any(p => p.Id == categoryProductDTO.ProductId) ||
                !context.Categories.Any(p => p.Id == categoryProductDTO.CategoryId))
            {
                return false;
            }

            return true;
        }

        private static void InitializeFilePath()
            => filePath = "../../../Datasets/categories-products.xml";
    }
}