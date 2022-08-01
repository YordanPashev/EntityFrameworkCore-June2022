namespace ProductShop
{
    using System.IO;
    using System.Xml.Linq;

    using XmlFacade;
    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.Dtos.Import;

    using AutoMapper;

    public class StartUp
    {
        private static string filePath;

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));

            InitializeFilePath();

            string inputXml = File.ReadAllText(filePath);
            string result = ImportUsers(context, inputXml);
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Load(filePath);
            string rootElementName = doc.Root.Name.LocalName;
            ImportUserDTO[] extractedUsers = XMLConverter.Deserializer<ImportUserDTO>(inputXml, rootElementName);
            User[] userToAdd = Mapper.Map<User[]>(extractedUsers);

            context.Users.AddRange(userToAdd);
            context.SaveChanges();

            int addedUsersCount = userToAdd.Length;
            return $"Successfully imported {addedUsersCount}";
        }

        private static void InitializeFilePath()
            => filePath = "../../../Datasets/users.xml";
    }
}