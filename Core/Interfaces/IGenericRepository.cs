using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec, PaginationParams paginationParams = null, bool includeDeleted = false);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(T entity);
        Task<int> GetCountAsync();
        T GetById(int id);
        IEnumerable<T> GetAll(ISpecification<T> spec, PaginationParams paginationParams = null, bool includeDeleted = false);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void SoftDelete(T entity);

    }
}
