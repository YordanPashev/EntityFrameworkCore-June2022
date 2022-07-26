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
            string date = Console.ReadLine();
            string result = GetBooksReleasedBefore(db, date);
            Console.WriteLine(result);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime givenDate;           
            bool isDateValid = DateTime.TryParseExact(date, "dd-MM-yyyy",
                                                      CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None, 
                                                      out givenDate);

            if (!isDateValid)
            {
                return string.Empty;
            }

            var filteredBooks = context.Books
                .Where(b => b.ReleaseDate < givenDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                })
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var bookInfo in filteredBooks)
            {
                output.AppendLine($"{bookInfo.Title} - {bookInfo.EditionType} - ${bookInfo.Price:F2}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
