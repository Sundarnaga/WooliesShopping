using System.Collections.Generic;

namespace Woolies.Shopping.Models
{
    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }

}
