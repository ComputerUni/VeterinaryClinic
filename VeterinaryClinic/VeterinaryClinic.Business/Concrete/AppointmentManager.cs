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
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> AppointmentAddAsync(Appointment appointment)
        {
            await _unitOfWork.Appointments.InsertAsync(appointment);
            await _unitOfWork.SaveAsync();
            return appointment;
        }

        public async Task AppointmentCancelAsync(Appointment appointment)
        {
            var result = await _unitOfWork.Appointments.GetAsync(x => x.Id == appointment.Id);
            if(result != null)
            {
                result.Status = AppointmentStatus.Cancelled;
                await _unitOfWork.Appointments.UpdateAsync(result);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task AppointmentUpdateAsync(Appointment appointment)
        {
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _unitOfWork.Appointments.GetAsync(x => x.Id == id);
        }

        public async Task<List<Appointment>> GetListAsync()
        {
            return await _unitOfWork.Appointments.ListAsync();
        }

        public async Task<string> UnitOfWorkTestMetodu(Appointment appointment)
        {
            await _unitOfWork.Appointments.InsertAsync(appointment);
            throw new Exception("Sistemde kasıtlı bir hata oluşturuldu! Unit of Work testi başlıyor.");
            await _unitOfWork.SaveAsync();
            return "Başarılı";
        }
    }
}
