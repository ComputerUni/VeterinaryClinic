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

        public async Task<Animal> AnimalAddAsync(Animal animal)
        {
            await _animalDal.InsertAsync(animal);
            return animal;
        }

        public async Task AnimalDeleteAsync(int id)
        {
            var deletedAnimal = await _animalDal.GetAsync(a => a.Id == id);
            if(deletedAnimal != null)
            {
                await _animalDal.DeleteAsync(deletedAnimal);
            }
            
        }

        public async Task AnimalUpdateAsync(Animal animal)
        {
            await _animalDal.UpdateAsync(animal);
        }

        public async Task<Animal> GetByIDAsync(int id)
        {
            var result = await _animalDal.GetAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<Animal>> GetListAsync()
        {
            return await _animalDal.ListAsync();
        }
    }
}
