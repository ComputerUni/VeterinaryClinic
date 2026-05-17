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
        Context c = new Context();
        DbSet<Treatment> _object;

        public void Delete(Treatment p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public Treatment Get(Expression<Func<Treatment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(Treatment p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<Treatment> List()
        {
            return _object.ToList();
        }

        public List<Treatment> List(Expression<Func<Treatment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(Treatment p)
        {
            _object.Update(p);
            c.SaveChanges();
        }
    }
}
