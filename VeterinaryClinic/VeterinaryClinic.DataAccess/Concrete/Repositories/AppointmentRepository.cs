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

        public async Task DeleteAsync(Appointment p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public async Task<Appointment> GetAsync(Expression<Func<Appointment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public async Task InsertAsync(Appointment p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public async Task<List<Appointment>> ListAsync()
        {
            return _object.ToList();
        }

        public async Task<List<Appointment>> ListAsync(Expression<Func<Appointment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public async Task UpdateAsync(Appointment p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
