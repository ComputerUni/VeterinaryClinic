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

        public void CalculateTotalAmount(Payment payment)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetList()
        {
            return _paymentDal.List();
        }

        public void PaymentAdd(Payment payment)
        {
            _paymentDal.Insert(payment);
        }

        public void PaymentUpdate(Payment payment)
        {
            _paymentDal.Update(payment);
        }
    }
}
