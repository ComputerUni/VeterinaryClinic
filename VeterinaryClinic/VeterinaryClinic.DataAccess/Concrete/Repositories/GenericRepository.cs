using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.DataAccess.Concrete.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _object;

        public GenericRepository(Context context)
        {
            _context = context;
            _object = _context.Set<T>();
        }

        public async Task DeleteAsync(T p)
        {
            var deleteEntity = _context.Entry(p);
            deleteEntity.State = EntityState.Deleted;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(T p)
        {
            var addedEntity = _context.Entry(p);
            addedEntity.State = EntityState.Added;
        }

        public async Task<List<T>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task <List<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(T p)
        {
            var updatedEntity = _context.Entry(p);
            updatedEntity.State = EntityState.Modified;
        }
    }
}
