using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Validation.Infrastructure
{
    public class MustBeTrueAttribute : Attribute, IModelValidator
    {
        // NOTE: The IModelValidator interface defines an IsRequired property, which is used to
        //       indicate whether validation by this class is required(which is a little misleading
        //       because the value returned by this property is simply used to order validation
        //       attributes so that the required ones are executed first).
        public bool IsRequired => true;

        public string ErrorMessage { get; set; } = "This value must be true";

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            bool? value = context.Model as bool?;
            if (!value.HasValue || value.Value == false)
            {
                return new List<ModelValidationResult> {new ModelValidationResult("", ErrorMessage)};
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
