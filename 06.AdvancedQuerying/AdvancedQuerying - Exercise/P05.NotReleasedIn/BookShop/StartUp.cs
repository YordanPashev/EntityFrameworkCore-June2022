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
            int releaseYearToIgnore = int.Parse(Console.ReadLine());
            string result = GetBooksNotReleasedIn(db, releaseYearToIgnore);
            Console.WriteLine(result);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            string[] filteredBooks = context.Books
                                         .Where(b => b.ReleaseDate.Value.Year != year)
                                         .OrderBy(b => b.BookId)
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
