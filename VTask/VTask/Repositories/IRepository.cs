﻿using Elfie.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VTask.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Remove(T entity);
        Task SaveChanges();
    }
}
