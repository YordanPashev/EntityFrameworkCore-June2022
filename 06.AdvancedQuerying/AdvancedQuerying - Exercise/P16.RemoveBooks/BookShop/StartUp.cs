namespace BookShop
{
    using System.Linq;
    using BookShop.Initializer;
    using BookShop.Models;
    using Data;
    using Z.EntityFramework.Plus;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            int removedBooksCount = RemoveBooks(db);
            System.Console.WriteLine(removedBooksCount);
        }

        public static int RemoveBooks(BookShopContext context)
        {
            Book[] booksToDelete = context.Books
                    .Where(b => b.Copies < 4200)
                    .ToArray();

            context.Books.RemoveRange(booksToDelete);
            
            context.SaveChanges();

            return booksToDelete.Count();

            ////Solution with Bulk
            //int deletedBooksCount = context.Books
            //    .Where(b => b.Copies < 4200)
            //    .Delete(p => new Book());

            //return deletedBooksCount;
        }
    }
}
