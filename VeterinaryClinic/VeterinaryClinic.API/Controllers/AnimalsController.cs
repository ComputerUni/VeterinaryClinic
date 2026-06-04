using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/animals")]
    public class AnimalsController : BaseController
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // 🎯 Burası tetiklendiğinde hata veriyorsa catch bloguna düşecek
                var result = await _animalService.GetListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // 🚨 Sinsi hatayı maskelemeyi bırak, ekrana neyse yazdır!
                return Ok(new
                {
                    Error = "Arka planda sinsi bir hata oluştu!",
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    Details = ex.StackTrace
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _animalService.GetByIDAsync(id);
            if (result == null)
            {
                return NotFound("Hayvan Bulunamadı");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromBody] Animal animal)
        {
            if(animal == null)
            {
                return BadRequest("Hayvan verisi boş olamaz");
            }
            var createdAnimal = await _animalService.AnimalAddAsync(animal);
            return StatusCode(201, createdAnimal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            if(id != animal.Id)
            {
                return BadRequest("Geçersiz Hayvan ID'si");
            }
            await _animalService.AnimalUpdateAsync(animal);
            return Ok(animal);
        }

    }
}
