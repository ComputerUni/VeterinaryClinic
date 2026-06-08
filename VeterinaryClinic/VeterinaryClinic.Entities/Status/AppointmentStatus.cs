using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Status
{
    public enum AppointmentStatus
    {
        [Display(Name ="Planlandı")]
        Scheduled = 1,
        [Display(Name = "Tamamlandı")]
        Completed = 2,
        [Display(Name = "İptal Edildi")]
        Cancelled = 3
    }
}
