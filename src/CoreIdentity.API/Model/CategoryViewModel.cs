using CoreIdentity.API.Model.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<CategoryViewModel> Children { get; set; }
        public long ParentId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CategoryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
