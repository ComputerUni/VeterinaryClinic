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

        public Task CalculateTotalAmountAsync(Payment payment)
        {
            throw new NotImplementedException();
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
