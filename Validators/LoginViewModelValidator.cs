using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.App_GlobalResources;
using Camping.ViewModels;
using FluentValidation;

namespace Camping.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(p => p.EmailLogin).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                            .EmailAddress().WithLocalizedMessage(() => Resource.WrongFormatEmail);
            RuleFor(p => p.PasswordLogin).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty);
        }      
    }
}