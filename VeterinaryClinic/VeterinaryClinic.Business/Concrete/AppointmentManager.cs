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

        public async Task<Appointment> AppointmentAddAsync(Appointment appointment)
        {
            await _appointmentDal.InsertAsync(appointment);
            return appointment;
        }

        public async Task AppointmentCancelAsync(Appointment appointment)
        {
            var result = await _appointmentDal.GetAsync(x => x.Id == appointment.Id);
            if(result != null)
            {
                result.Status = AppointmentStatus.Cancelled;
                await _appointmentDal.UpdateAsync(result);
            }
        }

        public async Task AppointmentUpdateAsync(Appointment appointment)
        {
            await _appointmentDal.UpdateAsync(appointment);
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _appointmentDal.GetAsync(x => x.Id == id);
        }

        public async Task<List<Appointment>> GetListAsync()
        {
            return await _appointmentDal.ListAsync();
        }
    }
}
