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

        public TreatmentRepository(Context context)
        {
            _context = context;
            _object = _context.Set<Treatment>();
        }

        public async Task DeleteAsync(Treatment p)
        {
            _object.Remove(p);
        }

        public async Task<Treatment> GetAsync(Expression<Func<Treatment, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(Treatment p)
        {
            await _object.AddAsync(p);
        }

        public async Task<List<Treatment>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task<List<Treatment>> ListAsync(Expression<Func<Treatment, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(Treatment p)
        {
            _object.Update(p);
        }
    }
}
