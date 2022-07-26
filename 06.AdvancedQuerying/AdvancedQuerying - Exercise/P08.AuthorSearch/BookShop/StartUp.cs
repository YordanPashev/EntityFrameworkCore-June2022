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
            string firstNameSuffix = Console.ReadLine();
            string result = GetAuthorNamesEndingIn(db, firstNameSuffix);
            Console.WriteLine(result);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string firstNameSuffix = input;

            var filteredAuthorsNames = context.Authors
                .Where(a => a.FirstName.EndsWith(firstNameSuffix))
                .Select(a => new { FullName = $"{a.FirstName} {a.LastName}" })
                .ToArray()
                .OrderBy(a => a.FullName);

            StringBuilder output = new StringBuilder();

            foreach (var authorFullName in filteredAuthorsNames)
            {
                output.AppendLine(authorFullName.FullName);
            }
            return output.ToString().TrimEnd();
        }
    }
}
