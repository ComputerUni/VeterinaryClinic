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
        private readonly Context _context;
        private readonly DbSet<User> _object;

        public async Task DeleteAsync(User p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public async Task InsertAsync(User p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public async Task<List<User>> ListAsync()
        {
            return _object.ToList();
        }

        public async Task<List<User>> ListAsync(Expression<Func<User, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public async Task UpdateAsync(User p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
