using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.Filters;
using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Culture]
    [Validator(typeof(EditPasswordViewModelValidator))]
    public class EditPasswordViewModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Error { get; set; }
    }
}