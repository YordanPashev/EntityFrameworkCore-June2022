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
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));

            InitializeFilePath();

            string inputXml = File.ReadAllText(filePath);
            string result = ImportCategories(context, inputXml);
            System.Console.WriteLine(result);
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            //XDocument doc = XDocument.Load(filePath);
            //string rootElementName = doc.Root.Name.LocalName;

            const string rootElementName = "Categories";

            ImportCategoryDTO[] extracedCategoriesFromXml = XMLConverter.Deserializer<ImportCategoryDTO>(inputXml, rootElementName);
            List<Category> validCategoriesToAdd = new List<Category>();

            foreach (var dto in extracedCategoriesFromXml)
            {
                if (dto.Name == null)
                {
                    continue;
                }

                var category = new Category
                {
                    Name = dto.Name
                };
                validCategoriesToAdd.Add(category);
            }

            //Alternative Solution with Automapper (NOT FOR JUDGE)
            //Category[] validCategoriesToAdd = Mapper.Map<Category[]>(extracedCategoriesFromXml.Where(c => c.Name != null));

            context.Categories.AddRange(validCategoriesToAdd);
            context.SaveChanges();

            int addedCategoriesCount = validCategoriesToAdd.Count;
            return $"Successfully imported {addedCategoriesCount}";
        }

        private static void InitializeFilePath()
            => filePath = "../../../Datasets/categories.xml";
    }
}