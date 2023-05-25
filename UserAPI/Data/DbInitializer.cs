using UserAPI.Models;

namespace UserAPI.Data;

public class DbInitializer : IDbInitializer
{
    // This method will create and seed the database.
    public void Initialize(UserApiContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Look for any Products
        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }

        List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Alex" }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}
