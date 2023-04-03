﻿
namespace Smartbell.Api.Contracts
{
    public interface IGenericRepository <T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
