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
            new Keyboard { Id = 1, Name = "Stellar6", Image = "https://i.ibb.co/vksYzX6/326039847-1198618761048215-6154956895303938374-n.jpg", ReservedBy = null },
            new Keyboard { Id = 2, Name = "Coolshirtz", Image = "https://i.ibb.co/f9RmdgL/334835993-212930388074122-6760579457068577911-n.jpg", ReservedBy = null },
            new Keyboard { Id = 3, Name = "Back&White", Image = "https://i.ibb.co/nBCST80/bluedark.jpg", ReservedBy = null },
            new Keyboard { Id = 4, Name = "Pastel", Image = "https://i.ibb.co/Lng1fZ3/fluffy.jpg", ReservedBy = null },
            new Keyboard { Id = 5, Name = "The Swiss Cheeseboard", Image = "https://i.ibb.co/VpCCV8c/moon.jpg", ReservedBy = null },
            new Keyboard { Id = 6, Name = "Command65", Image = "https://i.ibb.co/R4803FT/oldschool.jpg", ReservedBy = null },

        };

        context.Keyboards.AddRange(keyboards);
        context.SaveChanges();
    }
}
