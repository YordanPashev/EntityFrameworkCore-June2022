namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class StartUp
    {

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            string xmlStringResult = GetCategoriesByProductsCount(context);

            File.WriteAllText("../../../Results/categories-by-products.xml", xmlStringResult);

            Console.WriteLine(xmlStringResult);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            string rootElementName = "Categories";
            ExportCategoriesByProductsCount[] categoryProductsInfo = context
                                                            .Categories
                                                            .Include(c => c.CategoryProducts)
                                                            .ThenInclude(cp => cp.Product)
                                                            .Select(c => new ExportCategoriesByProductsCount
                                                            {
                                                                Name = c.Name,
                                                                Count = c.CategoryProducts.Count,
                                                                AveragePrice = c.CategoryProducts.Sum(cp => cp.Product.Price) / c.CategoryProducts.Count,
                                                                TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                                                            })
                                                            .OrderByDescending(c => c.Count)
                                                            .ThenBy(c => c.TotalRevenue)
                                                            .ToArray();

            string xmlStringResult = XMLConverter.Serialize(categoryProductsInfo, rootElementName);

            return xmlStringResult;
        }
    }
}