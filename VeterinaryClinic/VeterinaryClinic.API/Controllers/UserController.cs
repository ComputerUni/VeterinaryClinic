using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

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
        public IActionResult Register(User user)
        {
            var registeredUser = _userService.Register(user);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var loginUser = _userService.Login(user);
            if(loginUser == null)
            {
                return Unauthorized("E-posta veya şifre hatalı!");
            }
            var token = _tokenService.CreateToken(loginUser);
            return Ok(new { Token = token, Message = "Giriş Başarılı!" });
        }











    }
}
