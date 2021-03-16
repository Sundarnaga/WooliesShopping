using System;
using System.Collections.Generic;

namespace Woolies.Shopping.Models
{

    public class ShopperHistory
    {
        public int customerId { get; set; }
        public List<Product> products { get; set; }
    }



}
