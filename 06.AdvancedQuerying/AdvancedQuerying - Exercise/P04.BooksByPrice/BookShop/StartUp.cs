namespace BookShop
{
    using System;
    using System.Linq;
    using System.Text;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string result = GetBooksByPrice(db);
            Console.WriteLine(result);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            int minPrice = 40;
            var filteredBooks = context.Books
                                         .Where(b => b.Price > minPrice)
                                         .OrderByDescending(b => b.Price)
                                         .Select(b => new
                                         {
                                             Title = b.Title,
                                             Price = b.Price.ToString("F2"),
                                         })
                                         .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var book in filteredBooks)
            {
                output.AppendLine($"{book.Title} - ${book.Price}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
