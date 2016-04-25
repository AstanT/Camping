using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camping.App_GlobalResources;
using Camping.ViewModels;
using FluentValidation;

namespace Camping.Validators
{
    public class PassRecoveryViewModelValidator : AbstractValidator<PassRecoveryViewModel>
    {
        public PassRecoveryViewModelValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage(Resource.FieldCannotBeEmpty)
                .EmailAddress()
                .WithMessage(Resource.WrongFormatEmail);
        }
    }
}