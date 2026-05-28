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
    public class TreatmentRepository : ITreatmentDal
    {
        private readonly Context _context;
        private readonly DbSet<Treatment> _object;

        public async Task DeleteAsync(Treatment p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public async Task<Treatment> GetAsync(Expression<Func<Treatment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public async Task InsertAsync(Treatment p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public async Task<List<Treatment>> ListAsync()
        {
            return _object.ToList();
        }

        public async Task<List<Treatment>> ListAsync(Expression<Func<Treatment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public async Task UpdateAsync(Treatment p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
