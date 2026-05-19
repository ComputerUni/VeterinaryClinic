using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IUserService
    {
        void Login(string username, string password);
        void Register(string username, string password, string email);
        void Authorization(string username, string password);

    }
}
