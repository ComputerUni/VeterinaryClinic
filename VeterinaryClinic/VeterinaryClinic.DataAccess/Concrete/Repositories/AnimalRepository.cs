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

        public async Task DeleteAsync(Animal p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public async Task<Animal> GetAsync(Expression<Func<Animal, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public async Task InsertAsync(Animal p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public async Task <List<Animal>> ListAsync()
        {
            return _object.ToList();
        }

        public async Task<List<Animal>> ListAsync(Expression<Func<Animal, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public async Task UpdateAsync(Animal p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
