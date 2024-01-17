using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class OrderedProduct
    {
        public int FotoId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
