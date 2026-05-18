using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface AppointmentService
    {
        List<Appointment> GetList();
        void AppointmentAdd(Appointment appointment);
        void AppointmentCancel(Appointment appointment);
        void AppointmentUpdate(Appointment appointment);

    }
}
