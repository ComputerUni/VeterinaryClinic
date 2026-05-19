using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Concrete
{
    public class AnimalManager : IAnimalService
    {
        IAnimalDal _animalDal;

        public AnimalManager(IAnimalDal animalDal)
        {
            _animalDal = animalDal;
        }

        public void AnimalAdd(Animal animal)
        {
            _animalDal.Insert(animal);
        }

        public void AnimalDelete(Animal animal)
        {
            _animalDal.Delete(animal);
        }

        public void AnimalUpdate(Animal animal)
        {
            _animalDal.Update(animal);
        }

        public Animal GetByID(int id)
        {
            return _animalDal.Get(x => x.Id == id); 
        }

        public List<Animal> GetList()
        {
            return _animalDal.List();
        }
    }
}
