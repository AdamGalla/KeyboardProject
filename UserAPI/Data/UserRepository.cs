using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data;

public class UserRepository : IRepository<User>
{

    private readonly UserApiContext db;

    public UserRepository(UserApiContext context)
    {
        db = context;
    }

    User IRepository<User>.Add(User entity)
    {
        var newCustomer = db.Users.Add(entity).Entity;
        db.SaveChanges();
        return newCustomer;
    }

    void IRepository<User>.Edit(User entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        db.SaveChanges();
    }

    User IRepository<User>.Get(int id)
    {
        return db.Users.FirstOrDefault(p => p.Id == id)!;
    }

    User IRepository<User>.LogIn(string name)
    {
        return db.Users.FirstOrDefault(p => p.Name == name)!;
    }

    IEnumerable<User> IRepository<User>.GetAll()
    {
        return db.Users.ToList();
    }

    void IRepository<User>.Remove(int id)
    {
        var customer = db.Users.FirstOrDefault(p => p.Id == id);
        if (customer != null)
        {
            db.Users.Remove(customer);
            db.SaveChanges();
        }
    }
}
