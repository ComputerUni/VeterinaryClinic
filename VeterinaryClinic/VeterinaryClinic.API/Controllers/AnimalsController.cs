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
        public IActionResult GetAll()
        {
            var result = _animalService.GetList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _animalService.GetByID(id);
            if (result == null)
            {
                return NotFound("Hayvan Bulunamadı");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            _animalService.AnimalAdd(animal);
            return StatusCode(201, animal);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal animal)
        {
            if(id != animal.Id)
            {
                return BadRequest("Geçersiz Hayvan ID'si");
            }
            _animalService.AnimalUpdate(animal);
            return StatusCode(200, animal);
        }

    }
}
