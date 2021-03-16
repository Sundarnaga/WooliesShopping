using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.Interface.Validations
{
    public class TrolleyCalculatorValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                var trolleyRequest = value as TrolleyCalculatorRequest;                
                foreach (TrollyProduct trolley in trolleyRequest.products)
                {
                    if (trolley.Name == string.Empty)
                    {
                        return new ValidationResult("Product name can't be empty");
                    }
                }
                foreach (ProductQuantity qty in trolleyRequest.quantities)
                {
                    if (qty.Name == string.Empty)
                    {
                        return new ValidationResult("Product name can't be empty");
                    }
                }
                foreach (Special special in trolleyRequest.specials)
                {
                    if (special.Quantities == null)
                    {
                        return new ValidationResult("Quantity in special can't be null");
                    }
                    foreach(ProductQuantity qty in special.Quantities)
                        if (string.IsNullOrEmpty(qty.Name))
                        {
                            return new ValidationResult("Qty name can't be empty in specials");
                        }
                }
            }
            return ValidationResult.Success;

        }

    }
}
