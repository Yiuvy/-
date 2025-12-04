using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makarevich_sol_Domain.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdNormalizedName { get; set; }

        public string Adress { get; set; }
    }
}
