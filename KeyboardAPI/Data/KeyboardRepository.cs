using Microsoft.EntityFrameworkCore;
using KeyboardAPI.Models;

namespace KeyboardAPI.Data;

public class KeyboardRepository : IRepository<Keyboard>
{

    private readonly KeyboardApiContext db;

    public KeyboardRepository(KeyboardApiContext context)
    {
        db = context;
    }

    Keyboard IRepository<Keyboard>.Add(Keyboard entity)
    {
        var newKeyboard = db.Keyboards.Add(entity).Entity;
        db.SaveChanges();
        return newKeyboard;
    }

    void IRepository<Keyboard>.Edit(Keyboard entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        db.SaveChanges();
    }

    Keyboard IRepository<Keyboard>.Get(int id)
    {
        return db.Keyboards.FirstOrDefault(p => p.Id == id)!;
    }

    IEnumerable<Keyboard> IRepository<Keyboard>.GetAll()
    {
        return db.Keyboards.ToList();
    }

    void IRepository<Keyboard>.Remove(int id)
    {
        var Keyboard = db.Keyboards.FirstOrDefault(p => p.Id == id);
        if (Keyboard != null)
        {
            db.Keyboards.Remove(Keyboard);
            db.SaveChanges();
        }
    }
}
