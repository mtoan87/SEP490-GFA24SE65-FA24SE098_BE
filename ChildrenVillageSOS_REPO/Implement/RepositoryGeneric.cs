using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        public SoschildrenVillageDbContext _context;
        public DbSet<T> _dbSet;
        public RepositoryGeneric(SoschildrenVillageDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public DbSet<T> Entities()
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllNotDeletedAsync()
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return await _dbSet.Cast<ISoftDelete>().Where(e => e.IsDeleted == false).Cast<T>().ToListAsync();
            }

            return await _dbSet.ToListAsync(); // Nếu không hỗ trợ soft delete, trả về tất cả các bản ghi
        }

        public async Task<int> AddAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity is ISoftDelete softDeletableEntity) // cái này để check xem entity nào có IsDeleted không
            {
                if (softDeletableEntity.IsDeleted == true)
                {
                    // Thực hiện hard delete
                    _dbSet.Remove(entity);
                }
                else
                {
                    // Thực hiện soft delete
                    softDeletableEntity.IsDeleted = true;
                    _dbSet.Update(entity);
                }
            }
            else
            {
                // Thực hiện hard delete cho các bảng không có IsDeleted
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> RestoreAsync(T entity)
        {
            // cái này để check xem entity nào có IsDeleted không
            if (entity is ISoftDelete softDeletableEntity)
            {
                // Nếu đã bị soft delete, đặt lại IsDeleted thành false
                if (softDeletableEntity.IsDeleted == true)
                {
                    softDeletableEntity.IsDeleted = false; // Khôi phục lại entity
                    _dbSet.Update(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            throw new InvalidOperationException("This entity does not support soft delete or is not deleted.");
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

    }
}
