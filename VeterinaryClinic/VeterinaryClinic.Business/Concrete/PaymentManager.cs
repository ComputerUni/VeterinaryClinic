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
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public Task CalculateTotalAmountAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetListAsync()
        {
            return await _paymentDal.ListAsync();
        }

        public async Task<Payment> PaymentAddAsync(Payment payment)
        {
            await _paymentDal.InsertAsync(payment);
            return payment;
        }

        public async Task PaymentUpdateAsync(Payment payment)
        {
            await _paymentDal.UpdateAsync(payment);
        }
    }
}
