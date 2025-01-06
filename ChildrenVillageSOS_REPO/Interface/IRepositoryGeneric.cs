using ChildrenVillageSOS_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IRepositoryGeneric<T> where T : class
    {
        DbSet<T> Entities();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllNotDeletedAsync();
        Task<int> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //void Delete(T entity);
        Task<bool> RemoveAsync(T entity);
        Task SaveChangesAsync();
        Task<int> UpdateAsync(T entity);
        Task<bool> RestoreAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    }
}
