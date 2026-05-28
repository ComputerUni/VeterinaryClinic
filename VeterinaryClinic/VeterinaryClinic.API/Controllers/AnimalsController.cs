using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _animalService.GetListAsync();
            return Ok(result);
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
