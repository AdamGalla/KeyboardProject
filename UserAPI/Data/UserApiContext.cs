using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data;

public class UserApiContext : DbContext
{
    public UserApiContext(DbContextOptions<UserApiContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
