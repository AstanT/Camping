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
    public class AddServiceViewModelValidator : AbstractValidator<AddServiceViewModel>
    {
        public AddServiceViewModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.Price).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.Description).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.ClientMax).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.Prepaymant).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
            RuleFor(p => p.Photo).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty);
        }       
    }
}