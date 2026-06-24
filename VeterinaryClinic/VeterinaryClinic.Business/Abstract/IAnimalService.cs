using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IAnimalService
    {
        Task <List<Animal>> GetListAsync();
        Task<Animal> AnimalAddAsync(Animal animal);
        Task AnimalDeleteAsync(int id);
        Task AnimalUpdateAsync(Animal animal);
        Task<Animal> GetByIDAsync(int id);
        Task<List<Animal>> GetAnimalsByOwnerIdAsync(int ownerId);

    }
}
