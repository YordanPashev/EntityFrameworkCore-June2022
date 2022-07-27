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
            IncreasePrices(db);
        }

        public static void IncreasePrices(BookShopContext context)
        {
            IQueryable<Book> booksReleasedBefore2010 = context.Books
                    .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (Book book in booksReleasedBefore2010)
            {
                book.Price += 5;
            }

            context.SaveChanges();

            ////Solution with Bulk
            //context.Books
            //    .Where(b => b.ReleaseDate.Value.Year < 2010)
            //    .Update(p => new Book() { Price = p.Price + 5 });
        }  
    }
}
