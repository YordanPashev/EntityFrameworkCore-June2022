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
            string prefix = Console.ReadLine();
            string result = GetBooksByAuthor(db, prefix);
            Console.WriteLine(result);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {

            var filteredBooksInfo = context.Books
                .Include(a => a.Author)
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    Title = b.Title,
                    AuthorFullName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var bookInfo in filteredBooksInfo)
            {
                output.AppendLine($"{bookInfo.Title} ({bookInfo.AuthorFullName})");
            }
            return output.ToString().TrimEnd();
        }
    }
}
