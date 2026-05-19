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
    public class EfUserDal:GenericRepository<User>, IUserDal
    {
        public EfUserDal(Context context):base(context)
        {

        }
    }
}
