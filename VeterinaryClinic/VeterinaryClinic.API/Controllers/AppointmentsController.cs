using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/appointments")]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("Böyle bir randevu bulunamadı");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Randevu verisi boş olamaz");
            }

            await _appointmentService.AppointmentAddAsync(appointment);
            return Ok("Randevu başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AppointmentUpdate(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Geçersiz Randevu Id'si");
            }
            await _appointmentService.AppointmentUpdateAsync(appointment);
            return Ok("Randevu başarıyla güncellendi");
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> AppointmentCancel(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Geçersiz Randevu Id");
            }
            await _appointmentService.AppointmentCancelAsync(appointment);
            return Ok("Randevu başarıyla iptal edildi");
        }

        [HttpPost("test-uow")]
        public async Task<IActionResult> TestUnitOfWork([FromBody] Appointment appointment)
        {
            try
            {
                var result = await _appointmentService.UnitOfWorkTestMetodu(appointment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mesaj = "Hata yakalandı", Detay = ex.Message });
            }
        }

    }
}
