﻿namespace UserAPI.Data;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(int id);
    T LogIn(string name);
    T Add(T entity);
    void Edit(T entity);
    void Remove(int id);
}
