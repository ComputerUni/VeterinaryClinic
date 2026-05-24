using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Authorization(User user)
        {
            var existingUser = _userDal.Get(u => u.Id == user.Id);
            return existingUser;
        }

        public User GetUser(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        public User Login(User user)
        {
            var existingUser = _userDal.Get(u => u.Email == user.Email);

            if(existingUser != null)
            {
                if(BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
                {
                    return existingUser;
                }
            }
            return null;
        }

        
        public User Register(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _userDal.Insert(user);
            return user;
        }
    }
}
