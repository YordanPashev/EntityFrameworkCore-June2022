namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;


    public class StartUp
    {

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            string xmlStringResult = GetProductsInRange(context);

            File.WriteAllText("../../../Results/products-in-range.xml", xmlStringResult);

            Console.WriteLine(xmlStringResult);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            string rootElementName = "Products";
            ExportProductInRangeDTO[] productsInRange = context
                                                            .Products
                                                            .Where(p => p.Price >= 500 && p.Price <= 1000)
                                                            .OrderBy(p => p.Price)
                                                            .Select(p => new ExportProductInRangeDTO
                                                            {
                                                                Name = p.Name,
                                                                Price = p.Price,
                                                                BuyerFullName = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                                                            })
                                                            .Take(10)
                                                            .ToArray();

            string xmlStringResult = XMLConverter.Serialize(productsInRange, rootElementName);

            return xmlStringResult;
        }
    }
}