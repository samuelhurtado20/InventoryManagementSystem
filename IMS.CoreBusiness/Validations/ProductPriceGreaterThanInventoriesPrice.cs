using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness.Validations
{
    internal class ProductPriceGreaterThanInventoriesPrice : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Product? product = validationContext.ObjectInstance as Product;
            if(product is not null)
            {
                if (!product.ValidatePricing())
                {
                    //string?[] memberNames = new[] { validationContext.MemberName };

                    IEnumerable<string> memberNames = new List<string>();
                    memberNames.Append(validationContext.MemberName);
                    return new ValidationResult($"The product's price is less than summary of its inventories' price: {product.TotalInventoryCost()}", memberNames);
                }
            }
            return ValidationResult.Success;
        }
    }
}
