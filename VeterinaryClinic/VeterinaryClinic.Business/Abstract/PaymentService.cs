using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface PaymentService
    {
        List<Payment> GetList();
        void PaymentAdd(Payment payment);
        void PaymentUpdate(Payment payment);
        void CalculateTotalAmount(Payment payment);

    }
}
