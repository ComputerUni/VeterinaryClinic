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
    public class PaymentManager : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> CalculateTotalAmountAsync()
        {
            var payments = await _unitOfWork.Payments.ListAsync();
            return payments.Sum(p => p.AmountPaid);
        }

        public async Task<List<Payment>> GetByOwnerAsync(int id)
        {
            var myAnimal = (await _unitOfWork.Animals.ListAsync(a => a.OwnerId == id)).Select(a => a.Id).ToHashSet();
            var myAppointment = (await _unitOfWork.Appointments.ListAsync(a => myAnimal.Contains(a.AnimalId))).Select(a => a.Id).ToHashSet();
            return await _unitOfWork.Payments.ListAsync(p => myAppointment.Contains(p.AppointmentId));
        }

        public async Task<List<Payment>> GetByAppointmentIdAsync(int id)
        {
            return await _unitOfWork.Payments.ListAsync(x => x.AppointmentId == id);
        }

        public async Task<List<Payment>> GetListAsync()
        {
            return await _unitOfWork.Payments.ListAsync();
        }

        public async Task<Payment> PaymentAddAsync(Payment payment)
        {
            await _unitOfWork.Payments.InsertAsync(payment);
            await _unitOfWork.SaveAsync();
            return payment;
        }

        public async Task PaymentUpdateAsync(Payment payment)
        {
            await _unitOfWork.Payments.UpdateAsync(payment);
            await _unitOfWork.SaveAsync();
        }
    }
}
