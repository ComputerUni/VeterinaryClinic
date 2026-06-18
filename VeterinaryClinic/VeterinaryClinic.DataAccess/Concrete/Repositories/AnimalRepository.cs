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
    public class AnimalRepository : IAnimalDal
    {
        private readonly Context _context;
        private readonly DbSet<Animal> _object;

        public AnimalRepository(Context context)
        {
            _context = context;
            _object = _context.Set<Animal>();
        }

        public async Task DeleteAsync(Animal p)
        {
            _object.Remove(p);
        }

        public async Task<Animal> GetAsync(Expression<Func<Animal, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(Animal p)
        {
            await _object.AddAsync(p);
        }

        public async Task <List<Animal>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task<List<Animal>> ListAsync(Expression<Func<Animal, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(Animal p)
        {
            _object.Update(p);
        }
    }
}
