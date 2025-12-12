using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makarevich_sol_Domain.Entities
{
    public class CartItem
    {
        public Doctor Item { get; set; }
        public int Qty { get; set; } //quantity
    }
}
