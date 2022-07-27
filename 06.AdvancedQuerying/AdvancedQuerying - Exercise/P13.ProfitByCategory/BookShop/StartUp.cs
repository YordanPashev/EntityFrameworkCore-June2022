namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string auhorsBooksCopiesInfo = GetTotalProfitByCategory(db);
            Console.WriteLine(auhorsBooksCopiesInfo);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var bookByTotalProfil = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Select(b => b.Book.Price * b.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var booksInfo in bookByTotalProfil)
            {
                output.AppendLine($"{booksInfo.CategoryName} ${booksInfo.TotalProfit:F2}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
