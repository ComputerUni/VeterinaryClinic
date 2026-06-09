using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Status;

namespace VeterinaryClinic.Entities.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; }
    }
}
