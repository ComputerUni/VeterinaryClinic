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
        public async Task<IActionResult> Get()
        {
            var treatmentList = await _treatmentService.GetList();
            return Ok(treatmentList);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Treatment treatment)
        {
            var createdTreatment = await _treatmentService.TreatmentAdd(treatment);
            return Ok(createdTreatment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return BadRequest("Geçersiz tedavi ID'si");
            }
            await _treatmentService.TreatmentDelete(id);
            return Ok("Tedavi başarıyla silindi!");
        } 

    }
}
