using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;

namespace VeterinaryClinic.API.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _paymentService.GetListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Error = "Hata oluştu!",
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    Details = ex.StackTrace
                });
            }
        }

        [HttpGet("total")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTotal()
        {
            var total = await _paymentService.CalculateTotalAmountAsync();
            return Ok(total);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _paymentService.GetByAppointmentIdAsync(id);
            if(result == null)
            {
                return NotFound("Bu randevuya ait ödeme sistemi bulunamadı");
            }
            return Ok(result);
        }        

    }
}
