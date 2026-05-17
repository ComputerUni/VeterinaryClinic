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
        Context c = new Context();
        DbSet<Payment> _object;
        public void Delete(Payment p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public Payment Get(Expression<Func<Payment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(Payment p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<Payment> List()
        {
            return _object.ToList();
        }

        public List<Payment> List(Expression<Func<Payment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(Payment p)
        {
            _object.Update(p);
            c.SaveChanges();
        }
    }
}
