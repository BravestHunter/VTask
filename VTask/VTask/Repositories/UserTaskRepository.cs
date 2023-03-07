using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTask.Data;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly DefaultDbContext _dbContext;

        public UserTaskRepository(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserTask> Get(int id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<UserTask>> GetLast(int count)
        {
            return await _dbContext.Tasks.Take(count).ToArrayAsync();
        }

        public async Task<UserTask> Add(UserTask task)
        {
            _dbContext.Tasks.Add(task);
            task.User = await _dbContext.Users.FirstAsync();

            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<UserTask> Update(UserTask task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task Delete(UserTask task)
        {
            _dbContext.Tasks.Remove(task);

            await _dbContext.SaveChangesAsync();
        }
    }
}
