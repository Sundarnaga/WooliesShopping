using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Woolies.Shopping.Interface.Validations;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.Interface
{
    
    [TrolleyCalculatorValidation]
    public class TrolleyCalculatorRequest
    {
        [Required]
        public List<TrollyProduct> products { get; set; }
        [Required]
        public List<Special> specials { get; set; }
        [Required]
        public List<ProductQuantity> quantities { get; set; }
    }

}

