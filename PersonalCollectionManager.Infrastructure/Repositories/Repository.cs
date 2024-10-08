﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Infrastructure.Data;


namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly ILogger<Repository<T>> _logger;

        public Repository(AppDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting all entities of type {typeof(T).Name}");
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting entity of type {typeof(T).Name} by id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error finding entities of type {typeof(T).Name}");
                throw;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding entity of type {typeof(T).Name}");
                throw;
            }
        }


        public async Task<T> Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating entity of type {typeof(T).Name}");
                throw;
            }
        }

        public async Task Remove(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing entity of type {typeof(T).Name}");
                throw;
            }
        }


        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding entities of type {typeof(T).Name}");
                return null;
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                _context.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing entity of type {typeof(T).Name}");
            }

        }
        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().UpdateRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating entities of type {typeof(T).Name}");
                return false;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving changes to database");
                throw;
            }
        }
    }
}
