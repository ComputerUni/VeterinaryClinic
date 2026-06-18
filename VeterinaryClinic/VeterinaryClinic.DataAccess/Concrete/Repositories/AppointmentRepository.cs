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
    public class AppointmentRepository : IAppointmentDal
    {
        private readonly Context _context;
        private readonly DbSet<Appointment> _object;

        public AppointmentRepository(Context context)
        {
            _context = context;
            _object = _context.Set<Appointment>();
        }

        public async Task DeleteAsync(Appointment p)
        {
            _object.Remove(p);
        }

        public async Task<Appointment> GetAsync(Expression<Func<Appointment, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(Appointment p)
        {
            await _object.AddAsync(p);
        }

        public async Task<List<Appointment>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task<List<Appointment>> ListAsync(Expression<Func<Appointment, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(Appointment p)
        {
            _object.Update(p);
        }
    }
}
