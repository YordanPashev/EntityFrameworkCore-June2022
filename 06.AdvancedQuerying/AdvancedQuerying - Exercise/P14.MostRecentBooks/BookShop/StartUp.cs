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
            string auhorsBooksCopiesInfo = GetMostRecentBooks(db);
            Console.WriteLine(auhorsBooksCopiesInfo);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var cactegoriesInfo = context.Categories
                .Include(cb => cb.CategoryBooks)
                .ThenInclude(b => b.Book)
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TopThreeMostRecentBooks = c.CategoryBooks.Select(cb => new 
                                                            { 
                                                                cb.Book.Title, 
                                                                cb.Book.ReleaseDate
                                                            })
                                                            .OrderByDescending(t => t.ReleaseDate)
                                                            .Take(3)
                })
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var category in cactegoriesInfo)
            {
                output.AppendLine($"--{category.CategoryName}");

                foreach (var book in category.TopThreeMostRecentBooks)
                {
                    output.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return output.ToString().TrimEnd();
        }
    }
}
