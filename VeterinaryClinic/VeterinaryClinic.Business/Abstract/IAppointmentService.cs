using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetListAsync();
        Task<Appointment> GetByIdAsync(int id);
        Task<Appointment> AppointmentAddAsync(Appointment appointment);
        Task AppointmentCancelAsync(Appointment appointment);
        Task AppointmentUpdateAsync(Appointment appointment);

    }
}
