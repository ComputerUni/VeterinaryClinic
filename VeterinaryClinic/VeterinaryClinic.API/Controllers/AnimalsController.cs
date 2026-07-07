using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

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
                var result = await _animalService.GetListAsync();
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

        [Authorize(Roles = "Customer")]
        [HttpGet("my-animals")]
        public async Task<IActionResult> GetMyAnimals()
        {
            var userIdClaim = User.FindFirst("sub")?.Value;
            if(string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }
            var ownerId = int.Parse(userIdClaim);
            var result = await _animalService.GetAnimalsByOwnerIdAsync(ownerId);
            return Ok(result);
        }

        [HttpGet("manager-animals")]
        [Authorize (Roles ="Manager")]
        public async Task<IActionResult> GetAllManager()
        {
            var result = await _animalService.GetListAsync();
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddAnimal([FromBody] AnimalDto animalDto)
        {
            if(animalDto == null)
            {
                return BadRequest("Hayvan verisi boş olamaz");
            }

            if (!animalDto.OwnerId.HasValue || animalDto.OwnerId == 0)
            {
                return BadRequest("Lütfen geçerli bir hayvan sahibi (müşteri) seçiniz.");
            }

            var animal = new Animal
            {
                OwnerId = animalDto.OwnerId.Value,
                Name = animalDto.Name ?? string.Empty,
                Age = animalDto.Age ?? 0,
                Weight = animalDto.Weight ?? 0,
                Height = animalDto.Height ?? 0,
                Species = animalDto.Species ?? string.Empty,
                Breed = animalDto.Breed ?? string.Empty,
                MedicalHistory = animalDto.MedicalHistory ?? string.Empty
            };

            var createdAnimal = await _animalService.AnimalAddAsync(animal);
            return StatusCode(201, createdAnimal);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] AnimalDto animalDto)
        {
            if(id != animalDto.Id)
            {
                return BadRequest("Geçersiz Hayvan ID'si");
            }

            if(animalDto == null)
            {
                return BadRequest("Hayvan verisi boş olamaz");
            }

            if (!animalDto.OwnerId.HasValue || animalDto.OwnerId == 0)
            {
                return BadRequest("Lütfen geçerli bir hayvan sahibi (müşteri) seçiniz.");
            }

            var existingAnimal = await _animalService.GetByIDAsync(id);
            if(existingAnimal == null)
            {
                return NotFound("Güncellenmek istenen hayvan bulunamadı");
            }


            existingAnimal.OwnerId = animalDto.OwnerId.Value;
            existingAnimal.Name = animalDto.Name ?? string.Empty;
            existingAnimal.Age = animalDto.Age ?? 0;
            existingAnimal.Weight = animalDto.Weight ?? 0;
            existingAnimal.Height = animalDto.Height ?? 0;
            existingAnimal.Species = animalDto.Species ?? string.Empty;
            existingAnimal.Breed = animalDto.Breed ?? string.Empty;
            existingAnimal.MedicalHistory = animalDto.MedicalHistory ?? string.Empty;

            await _animalService.AnimalUpdateAsync(existingAnimal);
            return Ok(existingAnimal);
        }

    }
}
