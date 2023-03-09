﻿using System.Threading.Tasks;
using VTask.Data;

namespace VTask.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultDbContext _dbContext;

        public IUserRepository UserRepository { get; init; }
        public IUserTaskRepository UserTaskRepository { get; init; }

        public UnitOfWork(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
            UserTaskRepository = new UserTaskRepository(dbContext);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
