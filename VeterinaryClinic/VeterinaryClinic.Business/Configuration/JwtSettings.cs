using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Business.Configuration
{
    public class JwtSettings
    {
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String Key { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
