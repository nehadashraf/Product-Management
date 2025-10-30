using API.DAL.Context;
using API.DAL.Helpers;
using API.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T:class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public virtual async Task<PaginationResult<T>> GetAllAsync(PagingParams pagingParams)
        {
            var query = _dbSet.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public virtual Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

    }
}
