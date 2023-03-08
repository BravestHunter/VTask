﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTask.Data;

namespace VTask.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;

        public Repository(DefaultDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T?> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToArrayAsync();
        }

        public T Add(T entity)
        {
            var entityEntry = _dbSet.Add(entity);
            return entityEntry.Entity;
        }

        public T Update(T entity)
        {
            var entityEntry = _dbSet.Update(entity);
            return entityEntry.Entity;
        }

        public T Remove(T entity)
        {
            var entityEntry = _dbSet.Remove(entity);
            return entityEntry.Entity;
        }
    }
}
