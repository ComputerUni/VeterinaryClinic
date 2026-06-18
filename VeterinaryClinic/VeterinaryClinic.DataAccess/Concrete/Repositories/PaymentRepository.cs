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
    public class PaymentRepository : IPaymentDal
    {
        private readonly Context _context;
        private readonly DbSet<Payment> _object;

        public PaymentRepository(Context context)
        {
            _context = context;
            _object = _context.Set<Payment>();
        }

        public async Task DeleteAsync(Payment p)
        {
            _object.Remove(p);
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> filter)
        {
            return await _object.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(Payment p)
        {
            await _object.AddAsync(p);
        }

        public async Task <List<Payment>> ListAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task <List<Payment>> ListAsync(Expression<Func<Payment, bool>> filter)
        {
            return await _object.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(Payment p)
        {
            _object.Update(p);
        }
    }
}
