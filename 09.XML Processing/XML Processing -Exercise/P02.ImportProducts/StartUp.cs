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

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();
            //Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));

            InitializeFilePath();

            string inputXml = File.ReadAllText(filePath);
            string result = ImportProducts(context, inputXml);
            System.Console.WriteLine(result);
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            string rootElementName = "Products";

            ImportProductDTO[] extracedProductsFromXml = XMLConverter.Deserializer<ImportProductDTO>(inputXml, rootElementName);

            var productsToAdd = extracedProductsFromXml
                .Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    SellerId = p.SellerId,
                    BuyerId = p.BuyerId
                })
                .ToArray();

            //Alternative Solution with AutoMapper (NOT FOR JUDGE)
            //Product[] productsToAdd = Mapper.Map<Product[]>(extracedProductsFromXml);

            context.Products.AddRange(productsToAdd);
            context.SaveChanges();

            int addedUsersCount = productsToAdd.Length;
            return $"Successfully imported {addedUsersCount}";
        }

        private static void InitializeFilePath()
            => filePath = "../../../Datasets/products.xml";
    }
}