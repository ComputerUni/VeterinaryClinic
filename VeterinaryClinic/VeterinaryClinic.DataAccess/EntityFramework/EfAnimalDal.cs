using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.DataAccess.Concrete;
using VeterinaryClinic.DataAccess.Concrete.Repositories;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.DataAccess.EntityFramework
{
    public class EfAnimalDal : GenericRepository<Animal>, IAnimalDal
    {
        private readonly Context _context;
        public EfAnimalDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAnimalsWithOwnerAsync()
        {
            return await _context.Animals.Include(x => x.Owner).ToListAsync();
        }
    }
}
