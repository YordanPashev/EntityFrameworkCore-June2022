namespace BookShop
{
    using System;
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
            string categories = Console.ReadLine();
            string result = GetBooksByCategory(db, categories);
            Console.WriteLine(result);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToArray();
            
            string[] filteredBooks = context.BooksCategories
                .Include(bc => bc.Category)
                .Include(bc => bc.Book)
                .OrderBy(b => b.Book.Title)
                .Where(bc => categories.Contains(bc.Category.Name.ToLower()))
                .Select(b => b.Book.Title)
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
