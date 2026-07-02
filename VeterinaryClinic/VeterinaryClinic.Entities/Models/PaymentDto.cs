using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Status;

namespace VeterinaryClinic.Entities.Models
{
    public class PaymentDto
    {
        public int AppointmentId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentMethod { get; set; }
    }
}
