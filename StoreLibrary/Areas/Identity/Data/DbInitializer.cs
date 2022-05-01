/*using CodeWEB.Models;

namespace StoreLibrary.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreLibraryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }

            var Book = new Book[]
            {
            new Book{Title ="Carson",Pages =int.Parse("12"),Author ="Alexander",Category  ="Carson",Price =int.Parse("12"),Desc  ="Alexander", ImgUrl ="", StoreId =int.Parse("12")},
            };
            foreach (Book s in Book)
            {
                context.Book.Add(s);
            }
            context.SaveChanges();
        }

    }
}*/
