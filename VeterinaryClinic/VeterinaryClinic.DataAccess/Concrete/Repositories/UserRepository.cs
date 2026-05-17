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
    public class UserRepository : IUserDal
    {
        Context c = new Context();
        DbSet<User> _object;

        public void Delete(User p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(User p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<User> List()
        {
            return _object.ToList();
        }

        public List<User> List(Expression<Func<User, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(User p)
        {
            _object.Update(p);
            c.SaveChanges();
        }
    }
}
