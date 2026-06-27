using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/treatments")]
    public class TreatmentsController : BaseController
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentsController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Get()
        {
            var treatmentList = await _treatmentService.GetListAsync();
            return Ok(treatmentList);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Add([FromBody] Treatment treatment)
        {
            var createdTreatment = await _treatmentService.TreatmentAddAsync(treatment);
            return Ok(createdTreatment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Geçersiz tedavi ID'si");
            }
            await _treatmentService.TreatmentDeleteAsync(id);
            return Ok("Tedavi başarıyla silindi!");
        } 

    }
}
