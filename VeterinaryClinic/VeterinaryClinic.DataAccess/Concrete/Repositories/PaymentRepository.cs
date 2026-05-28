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

        public async Task DeleteAsync(Payment p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public async Task InsertAsync(Payment p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public async Task <List<Payment>> ListAsync()
        {
            return _object.ToList();
        }

        public async Task <List<Payment>> ListAsync(Expression<Func<Payment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public async Task UpdateAsync(Payment p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
