namespace BookShop
{
    using System;
    using System.Linq;
    using System.Text;
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            string ageGroup = Console.ReadLine();
            string result = GetBooksByAgeRestriction(db, ageGroup);
            Console.WriteLine(result);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            string ageGroup = command.First().ToString().ToUpper() + string.Join("", command.ToLower().Skip(1));
            AgeRestriction ageRestriction = (AgeRestriction)Enum.Parse (typeof(AgeRestriction), ageGroup);
            var filteredBooks = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (string book in filteredBooks)
            {
                output.AppendLine(book);
            }

            return output.ToString().TrimEnd();
        }
    }
}
