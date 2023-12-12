﻿using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MagicVilla.MagicApi.Repository.IRepository;
namespace MagicVilla.MagicApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool track = true)
        {
            IQueryable<T> query = dbSet;
            if (!track)
            {
                query = query.AsNoTracking();

            }
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

      
    }
}
