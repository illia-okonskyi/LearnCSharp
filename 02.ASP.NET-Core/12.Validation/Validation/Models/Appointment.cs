using System;
using System.ComponentModel.DataAnnotations;
using Validation.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Validation.Models
{
    public class Appointment
    {
        // NOTE: There are several validation attributes which can be used to annotate fields:
        //       - Compare - ensures that properties must have the same value, which is useful when
        //                   you ask the user to provide the same information twice, such as an
        //                   e-mail address or a password.
        //       - Range - ensures that a numeric value(or any property type that implements
        //                 IComparable) does not lie beyond the specified minimum and maximum
        //                 values.
        //       - RegularExpression - ensures that a string value matches the specified regular
        //                             expression pattern. Note that the pattern has to match the
        //                             entire user-supplied value, not just a substring within it.
        //       - Required - ensures that the value is not empty or a string consisting only of
        //                    spaces.If you want to treat whitespace as valid, use
        //                    [Required(AllowEmptyStrings = true)].
        //       - StringLength - ensures that a string value is no longer than a specified maximum
        //                        length.You can also specify a minimum length.
        [Required]
        [Display(Name = "name")]
        public string ClientName { get; set; }

        [UIHint("Date")]
        [Required(ErrorMessage = "Please enter a date")]
        // NOTE: Remove validation is the validation via AJAX request. Is made automatically by the
        //       jQuery-validate plugin
        // NOTE: Documentation of the RemoteAttribute says that HttpMethod  can be only "Get" or
        //       "Post" (case sensetive) but it seems that comparision are made via case insensetive
        //       method, so "GET" or "POST" values are also accepatble.
        [Remote("ValidateDate", "Home", HttpMethod = "Post")]
        public DateTime Date { get; set; }

        [MustBeTrue(ErrorMessage = "You must accept the terms")]
        public bool TermsAccepted { get; set; }
    }
}
