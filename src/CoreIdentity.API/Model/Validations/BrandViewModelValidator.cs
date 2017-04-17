using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model.Validations
{
    public class BrandViewModelValidator : AbstractValidator<BrandViewModel>
    {
        public BrandViewModelValidator()
        {
            RuleFor(x=> x.BrandName).NotEmpty().WithMessage("Category Name cannot be empty")
                .Length(10,200).WithMessage("minimum 8 character and maximum 200 character");
            RuleFor(x => x.Description).Length(500).WithMessage("Description max 500 character");
        }
    }
}
