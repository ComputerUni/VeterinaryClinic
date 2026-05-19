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
        List<Animal> GetList();
        void AnimalAdd(Animal animal);
        void AnimalDelete(Animal animal);
        void AnimalUpdate(Animal animal);
        Animal GetByID(int id);

    }
}
