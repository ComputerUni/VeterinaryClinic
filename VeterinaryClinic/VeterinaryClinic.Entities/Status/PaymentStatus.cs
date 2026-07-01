using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Status
{
    public enum PaymentStatus
    {
        [Display(Name = "Nakit")]
        Cash = 1,

        [Display(Name = "Kredi Kartı")]
        CreditCard = 2,

        [Display(Name = "Havale")]
        BankTransfer = 3,




    }
}
