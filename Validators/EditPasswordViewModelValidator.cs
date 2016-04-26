using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.App_GlobalResources;
using Camping.Filters;
using Camping.ViewModels;
using FluentValidation;

namespace Camping.Validators
{
    [Culture]
    public class EditPasswordViewModelValidator : AbstractValidator<EditPasswordViewModel>
    {
        public EditPasswordViewModelValidator()
        {
            RuleFor(p => p.Password).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty)
                .Length(6, 20).WithMessage(Resource.PasswordLenght);
            RuleFor(p => p.ConfirmNewPassword).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty)
                .Equal(p => p.NewPassword).WithMessage(Resource.PassMismatch);
        }
    }
}