using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec, PaginationParams paginationParams = null, bool includeDeleted = false)
        {
            // IQueryable başlangıcı
            IQueryable<T> query = _dbSet;

            // Eğer soft delete olan kayıtlar hariç olsun istiyorsak, IsDeleted kontrolü eklenir
            if (!includeDeleted)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            // Eğer filtreleme kriteri varsa uygulayalım
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // Eğer include edilen navigation property'ler varsa ekleyelim
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Pagination varsa, burada pagination işlemini yapalım
            if (paginationParams != null)
            {
                query = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                             .Take(paginationParams.PageSize);
            }

            // Son olarak query'i çalıştır ve sonucu döndür
            return await query.ToListAsync();

            //var pagedEntities = await _genericRepository.GetAsync(spec, new PaginationParams { PageNumber = 1, PageSize = 10 }, false);
            //var allEntities = await _genericRepository.GetAsync(spec, null, true);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(ISpecification<T> spec, PaginationParams paginationParams = null, bool includeDeleted = false)
        {
            // IQueryable başlangıcı
            IQueryable<T> query = _dbSet;

            // Eğer soft delete olan kayıtlar hariç olsun istiyorsak, IsDeleted kontrolü eklenir
            if (!includeDeleted)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            // Eğer filtreleme kriteri varsa uygulayalım
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // Eğer include edilen navigation property'ler varsa ekleyelim
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Pagination varsa, burada pagination işlemini yapalım
            if (paginationParams != null)
            {
                query = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                             .Take(paginationParams.PageSize);
            }

            // Son olarak query'i çalıştır ve sonucu döndür
            return query.ToList();
        }
        
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        
    }
}
