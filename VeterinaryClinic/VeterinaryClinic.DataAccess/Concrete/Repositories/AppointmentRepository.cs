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
        Context c = new Context();
        DbSet<Appointment> _object;

        public void Delete(Appointment p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public Appointment Get(Expression<Func<Appointment, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(Appointment p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<Appointment> List()
        {
            return _object.ToList();
        }

        public List<Appointment> List(Expression<Func<Appointment, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(Appointment p)
        {
            _object.Update(p);
            c.SaveChanges();
        }
    }
}
