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
            DbInitializer.ResetDatabase(db);
            string result = GetGoldenBooks(db);
            Console.WriteLine(result);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            int maxCopies = 5000;

            string[] goldenEditionBooks = context.Books
                .Where(b => b.Copies < maxCopies &&
                            b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (string bookTitle in goldenEditionBooks)
            {
                output.AppendLine(bookTitle);
            }

            return output.ToString().TrimEnd();
    }
}
