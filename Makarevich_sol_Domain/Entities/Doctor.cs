using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makarevich_sol_Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int AmountOfPatients { get; set; }
        public string? Image { get; set; }
        public int? IdClinic { get; set; }
        public string?  Specialization { get; set; }
    }
}
