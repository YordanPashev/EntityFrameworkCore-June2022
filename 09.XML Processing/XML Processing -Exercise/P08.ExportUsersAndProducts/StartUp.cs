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

            string xmlStringResult = GetUsersWithProducts(context);

            File.WriteAllText("../../../Results/users-and-products.xml", xmlStringResult);

            Console.WriteLine(xmlStringResult);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            string rootElementName = "Users";
            ExportUserProductsDTO[] usersSoldProducts = context
                                                            .Users
                                                            .Include(ps => ps.ProductsSold)
                                                            .Where(u => u.ProductsSold.Any())
                                                            .OrderByDescending(u => u.ProductsSold.Count)
                                                            .ToArray()
                                                            .Select(u => new ExportUserProductsDTO
                                                            {
                                                                FirstName = u.FirstName,
                                                                LastName = u.LastName,
                                                                Age = u.Age,
                                                                SoldProducts = new ExportSoldProductsCountDTO()
                                                                {
                                                                    Count = u.ProductsSold.Count,
                                                                    Products = u.ProductsSold.Select(ps => new ExportProductNameAndPriceDTO
                                                                    {
                                                                        Name = ps.Name,
                                                                        Price = ps.Price,
                                                                    })
                                                                    .OrderByDescending(p => p.Price)
                                                                    .ToArray()
                                                                }
                                                            })
                                                            .Take(10)
                                                            .ToArray();

            ExportUsersAndTheirCountDTO usersResult = new ExportUsersAndTheirCountDTO()
            {
                Count = context.Users.Count(x => x.ProductsSold.Any()),
                Users = usersSoldProducts
            };
            string xmlStringResult = XMLConverter.Serialize(usersResult, rootElementName);

            return xmlStringResult;
        }
    }
}