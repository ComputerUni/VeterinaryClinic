using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Concrete
{
    public class Treatment
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string TreatmentType { get; set; }
        public string Notes { get; set; }
        public decimal Cost { get; set; }
    }
}
