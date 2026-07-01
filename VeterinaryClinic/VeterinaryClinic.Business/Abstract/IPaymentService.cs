using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetListAsync();
        Task<Payment> PaymentAddAsync(Payment payment);
        Task PaymentUpdateAsync(Payment payment);
        Task<List<Payment>> GetByAppointmentIdAsync(int id);
        Task<decimal> CalculateTotalAmountAsync();
    }
}
