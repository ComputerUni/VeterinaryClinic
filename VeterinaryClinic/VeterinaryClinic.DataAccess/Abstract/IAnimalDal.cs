using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.DataAccess.Abstract
{
    public interface IAnimalDal : IGenericRepository<Animal>
    {
        Task<List<Animal>> GetAnimalsWithOwnerAsync();
    }
}
