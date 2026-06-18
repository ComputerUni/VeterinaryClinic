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

        //Alttaki lambda işaretiyle üstteki uzun get ifadesi aynı anlama geliyor. 
        //public IAnimalDal Animals
        //{
        //    get
        //    {
        //        return _animalDal ??= new EfAnimalDal(_context);
        //    }
        //}
        public IAnimalDal Animals => _animalDal ??= new EfAnimalDal(_context);
        public IAppointmentDal Appointments => _appointmentDal ??= new EfAppointmentDal(_context);
        public IPaymentDal Payments => _paymentDal ??= new EfPaymentDal(_context);
        public ITreatmentDal Treatments => _treatmentDal ??= new EfTreatmentDal(_context);
        public IUserDal Users => _userDal ??= new EfUserDal(_context);


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
