using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var registeredUser = await _userService.Register(model);
            if(registeredUser.Succeeded)
            {
                return Ok(new {Message = "Kullanıcı ve Rol başarıyla oluşturuldu."});
            }
            return BadRequest(registeredUser.Errors);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var loginUser = await _userService.Login(model);
            if(!loginUser.Succeeded)
            {
                return Unauthorized(new { Message = "Kullanıcı adı veya şifre hatalı!" });
            }
            var user = await _userService.GetUserByUsername(model.Username);
            var token = await _tokenService.CreateToken(user);
            return Ok(new { Token = token, Message = "Giriş Başarılı!" });
        }

    }
}
