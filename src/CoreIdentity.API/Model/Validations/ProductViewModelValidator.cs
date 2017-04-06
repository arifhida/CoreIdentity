using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model.Validations
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(x => x.SKU).NotEmpty().WithMessage("SKU cannot be empty").Length(1, 100)
                .WithMessage("minimum 1 character and maximum 100 character");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name cannot be empty")
                .Length(10, 250).WithMessage("minimum 10 character and maximum 250 character");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Description cannot be empty")
                .Length(10, 550).WithMessage("minimum 10 character and maximum 550 character");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Unit price cannot be empty");
        }
    }
}
