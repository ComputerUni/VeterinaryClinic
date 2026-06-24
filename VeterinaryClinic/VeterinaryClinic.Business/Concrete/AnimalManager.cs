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
        private readonly IUnitOfWork _unitOfWork;

        public AnimalManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Animal> AnimalAddAsync(Animal animal)
        {
            await _unitOfWork.Animals.InsertAsync(animal);
            await _unitOfWork.SaveAsync();
            return animal;
        }

        public async Task AnimalDeleteAsync(int id)
        {
            var deletedAnimal = await _unitOfWork.Animals.GetAsync(a => a.Id == id);
            if(deletedAnimal != null)
            {
                await _unitOfWork.Animals.DeleteAsync(deletedAnimal);
                await _unitOfWork.SaveAsync();
            }
            
        }

        public async Task AnimalUpdateAsync(Animal animal)
        {
            await _unitOfWork.Animals.UpdateAsync(animal);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Animal> GetByIDAsync(int id)
        {
            var result = await _unitOfWork.Animals.GetAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<Animal>> GetListAsync()
        {
            return await _unitOfWork.Animals.ListAsync();
        }

        public async Task<List<Animal>> GetAnimalsByOwnerIdAsync(int ownerId)
        {
            return await _unitOfWork.Animals.ListAsync(a => a.OwnerId == ownerId);

        }
    }
}
