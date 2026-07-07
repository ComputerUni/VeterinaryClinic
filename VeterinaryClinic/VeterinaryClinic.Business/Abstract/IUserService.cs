using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IUserService
    {
        Task<IdentityResult> Register(RegisterDto model);
        Task<SignInResult> Login(LoginDto model);
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetCustomersAsync();

    }
}
