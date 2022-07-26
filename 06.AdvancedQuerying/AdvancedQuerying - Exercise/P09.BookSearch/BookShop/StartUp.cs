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
            string substring = Console.ReadLine();
            string result = GetBookTitlesContaining(db, substring);
            Console.WriteLine(result);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {

            string[] filteredBooks = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (string bookTitle in filteredBooks)
            {
                output.AppendLine(bookTitle);
            }
            return output.ToString().TrimEnd();
        }
    }
}
