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
        Context c = new Context();
        DbSet<Animal> _object;

        public void Delete(Animal p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public Animal Get(Expression<Func<Animal, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(Animal p)
        {
            _object.Add(p);
            c.SaveChanges();
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
            c.SaveChanges();
        }
    }
}
