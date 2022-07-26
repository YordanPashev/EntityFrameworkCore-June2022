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
            string auhorsBooksCopiesInfo = CountCopiesByAuthor(db);
            Console.WriteLine(auhorsBooksCopiesInfo);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorsTotalBookCopiesInfo = context.Authors
                .Include(a => a.Books)
                .Select(a => new 
                {
                    AuthorFullName = $"{a.FirstName} {a.LastName}",
                    TotalBookCopies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.TotalBookCopies)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var authorInfo in authorsTotalBookCopiesInfo)
            {
                output.AppendLine($"{authorInfo.AuthorFullName} - {authorInfo.TotalBookCopies}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
