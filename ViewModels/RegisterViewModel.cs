using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNomber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Error { get; set; }
    }
}