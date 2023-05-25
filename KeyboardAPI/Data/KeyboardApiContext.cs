using Microsoft.EntityFrameworkCore;
using KeyboardAPI.Models;

namespace KeyboardAPI.Data;

public class KeyboardApiContext : DbContext
{
    public KeyboardApiContext(DbContextOptions<KeyboardApiContext> options)
        : base(options)
    {
    }

    public DbSet<Keyboard> Keyboards { get; set; }
}
