using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAnimalDal Animals { get; }
        IAppointmentDal Appointments { get; }
        ITreatmentDal Treatments { get; }
        IPaymentDal Payments { get; }
        IUserDal Users { get; }

        Task<int> SaveAsync();
    }
}
