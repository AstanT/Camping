using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.App_GlobalResources;
using Camping.ViewModels;
using FluentValidation;

namespace Camping.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                .Length(1, 20).WithLocalizedMessage(() => Resource.Length);
            RuleFor(p => p.LastName).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                .Length(1, 20).WithLocalizedMessage(() => Resource.Length);
            RuleFor(p => p.Email).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                .EmailAddress().WithLocalizedMessage(() => Resource.WrongFormatEmail);
            RuleFor(p => p.Password).NotEmpty().WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                .Length(6, 20).WithLocalizedMessage(() => Resource.PasswordLenght);
            RuleFor(p => p.ConfirmPassword)
                .NotEmpty()
                .WithLocalizedMessage(() => Resource.FieldCannotBeEmpty)
                .Equal(p => p.Password)
                .WithLocalizedMessage(() => Resource.PassMismatch);
        }
    }
}