﻿using MagicVilla.MagicApi.Model;
using System.Linq.Expressions;

namespace MagicVilla.MagicApi.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool track = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task Save();
    }
}
