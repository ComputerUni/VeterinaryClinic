using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _appointmentService.GetList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _appointmentService.GetById(id);
            if (result == null)
            {
                return NotFound("Böyle bir randevu bulunamadı");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAppointment(Appointment appointment)
        {
            _appointmentService.AppointmentAdd(appointment);
            return Ok("Randevu başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult AppointmentUpdate(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Geçersiz Hayvan Id'si");
            }
            _appointmentService.AppointmentUpdate(appointment);
            return Ok("Randevu başarıyla güncellendi");
        }

        [HttpPut("{id}/cancel")]
        public ActionResult AppointmentCancel(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Geçersiz Randevu Id");
            }
            _appointmentService.AppointmentCancel(appointment);
            return Ok("Randevu başarıyla iptal edildi");
        }

    }
}
