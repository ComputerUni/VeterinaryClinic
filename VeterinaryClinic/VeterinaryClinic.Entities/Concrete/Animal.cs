using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Concrete
{
    public class Animal
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual User? Owner { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string MedicalHistory { get; set; }

    }
}
