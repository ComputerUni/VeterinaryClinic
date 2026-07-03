using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/payments")]
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

        [HttpGet("my-payments")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var ownerId = int.Parse(userId);
            var result = await _paymentService.GetByOwnerAsync(ownerId);
            return Ok(result);
        }


        //[HttpGet("{id}")]
        //[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> GetAppointmentById(int id)
        //{
        //    var result = await _paymentService.GetByAppointmentIdAsync(id);
        //    if (result == null)
        //    {
        //        return NotFound("Bu randevuya ait ödeme bulunamadı");
        //    }
        //    return Ok(result);
        //}

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddPayments([FromBody] Payment payment)
        {
            var result = await _paymentService.PaymentAddAsync(payment);
            return Ok(result);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdatePayments(int id, [FromBody] Payment payment)
        {
            if(id != payment.Id)
            {
                return BadRequest("Geçersiz ödeme ID'si");
            }

            await _paymentService.PaymentUpdateAsync(payment);
            return Ok("Ödeme başarıyla güncellendi");
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetPaymentId(int id)
        {
            var payments = await _paymentService.GetListAsync();
            var result = payments.FirstOrDefault(p => p.Id == id);
            if(result == null)
            {
                return NotFound("Bu ID'yw ait ödeme bulunamadı");
            }
            return Ok(result);
        }


    }
}
