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
    public class AnimalRepository : IAnimalDal
    {
        private readonly Context _context;
        private readonly DbSet<Animal> _object;

        public void Delete(Animal p)
        {
            _object.Remove(p);
            _context.SaveChanges();
        }

        public Animal Get(Expression<Func<Animal, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(Animal p)
        {
            _object.Add(p);
            _context.SaveChanges();
        }

        public List<Animal> List()
        {
            return _object.ToList();
        }

        public List<Animal> List(Expression<Func<Animal, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(Animal p)
        {
            _object.Update(p);
            _context.SaveChanges();
        }
    }
}
