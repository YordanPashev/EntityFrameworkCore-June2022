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
            int length = int.Parse(Console.ReadLine());
            int booksCount = CountBooks(db, length);
            Console.WriteLine($"There are {booksCount} books with longer title than {length} symbols");
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int booksCountResult = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return booksCountResult;
        }
    }
}
