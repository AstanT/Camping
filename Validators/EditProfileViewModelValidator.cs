using Camping.App_GlobalResources;
using Camping.Filters;
using Camping.ViewModels;
using FluentValidation;

namespace Camping.Validators
{
    [Culture]
    public class EditProfileViewModelValidator : AbstractValidator<EditProfileViewModel>
    {
        public EditProfileViewModelValidator ()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty)
                .Length(1, 20).WithMessage(Resource.Length);
            RuleFor(p => p.LastName).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty)
                .Length(1, 20).WithMessage(Resource.Length);
            RuleFor(p => p.Email).NotEmpty().WithMessage(Resource.FieldCannotBeEmpty)
                .EmailAddress().WithMessage(Resource.WrongFormatEmail);
            RuleFor(p => p.PhoneNomber).Length(1, 20).WithMessage(Resource.Length);
        }
    }
}