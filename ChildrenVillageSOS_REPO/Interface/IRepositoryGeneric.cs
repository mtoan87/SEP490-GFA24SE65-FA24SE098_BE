using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IRepositoryGeneric<T> where T : class
    {
        DbSet<T> Entities();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> RemoveAsync(T entity);
        Task SaveChangesAsync();
        Task<int> UpdateAsync(T entity);
    }
}
