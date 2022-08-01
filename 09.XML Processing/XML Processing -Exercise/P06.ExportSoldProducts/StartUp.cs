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

            string xmlStringResult = GetSoldProducts(context);

            File.WriteAllText("../../../Results/users-sold-products.xml", xmlStringResult);

            Console.WriteLine(xmlStringResult);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            string rootElementName = "Users";
            ExportUserSoldProductsInfoDTO[] usersSoldProducts = context
                                                            .Users
                                                            .Include(u => u.ProductsSold)
                                                            .Where(u => u.ProductsSold.Any(ps => ps.BuyerId.HasValue))
                                                            .OrderBy(u => u.LastName)
                                                            .ThenBy(u => u.FirstName)
                                                            .Select(u => new ExportUserSoldProductsInfoDTO
                                                            {
                                                                FirstName = u.FirstName,
                                                                LastName = u.LastName,
                                                                ProductsSold = u.ProductsSold.Select(u => new ExportSoldProductDTOcs
                                                                {
                                                                    Name = u.Name,
                                                                    Price = u.Price,
                                                                })
                                                                .ToArray()
                                                            })
                                                            .Take(5)
                                                            .ToArray();

            string xmlStringResult = XMLConverter.Serialize(usersSoldProducts, rootElementName);

            return xmlStringResult;
        }
    }
}