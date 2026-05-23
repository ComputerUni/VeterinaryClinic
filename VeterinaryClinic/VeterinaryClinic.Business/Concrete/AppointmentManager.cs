using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Status;

namespace VeterinaryClinic.Business.Concrete
{
    public class AppointmentManager : IAppointmentService
    {
        IAppointmentDal _appointmentDal;

        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void AppointmentAdd(Appointment appointment)
        {
            _appointmentDal.Insert(appointment);
        }

        public void AppointmentCancel(Appointment appointment)
        {
            var result = _appointmentDal.Get(x => x.Id == appointment.Id);
            if(result != null)
            {
                result.Status = AppointmentStatus.Cancelled;
                _appointmentDal.Update(result);
            }
        }

        public void AppointmentUpdate(Appointment appointment)
        {
            _appointmentDal.Update(appointment);
        }

        public Appointment GetById(int id)
        {
            return _appointmentDal.Get(x => x.Id == id);
        }

        public List<Appointment> GetList()
        {
            return _appointmentDal.List();
        }
    }
}
