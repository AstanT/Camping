using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Validator(typeof(EditProfileViewModelValidator))]
    public class EditProfileViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNomber { get; set; }
        public string Photo { get; set; }
    }
}