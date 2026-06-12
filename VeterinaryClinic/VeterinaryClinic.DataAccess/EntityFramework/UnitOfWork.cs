using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.DataAccess.Concrete;

namespace VeterinaryClinic.DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private IAnimalDal _animalDal;
        private IAppointmentDal _appointmentDal;
        private IPaymentDal _paymentDal;
        private ITreatmentDal _treatmentDal;
        private IUserDal _userDal;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public IAnimalDal Animals { get; }
        public IAppointmentDal Appointments { get; }
        public IPaymentDal Payments { get; }
        public ITreatmentDal Treatments { get; }
        public IUserDal Users { get; }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
