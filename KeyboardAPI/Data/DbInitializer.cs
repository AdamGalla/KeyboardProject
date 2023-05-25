using KeyboardAPI.Models;

namespace KeyboardAPI.Data;

public class DbInitializer : IDbInitializer
{
    // This method will create and seed the database.
    public void Initialize(KeyboardApiContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Look for any Products
        if (context.Keyboards.Any())
        {
            return;   // DB has been seeded
        }

        List<Keyboard> keyboards = new List<Keyboard>
        {
            new Keyboard { Id = 1, Name = "Adam", Url = "" }
        };

        context.Keyboards.AddRange(keyboards);
        context.SaveChanges();
    }
}
