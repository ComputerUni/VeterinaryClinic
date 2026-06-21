using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.Business.Concrete
{
    public class UserManager : IUserService
    {
        //private readonly IUserDal _userDal;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            //_userDal = userDal;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, model.Password, false);
            return result;
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var newUser = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Customer");
            }

            return result;

        }
    }
}
