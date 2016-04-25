using Camping.Filters;
using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Culture]
    [Validator(typeof(PassRecoveryViewModelValidator))]
    public class PassRecoveryViewModel
    {
        public string Email { get; set; }
        public string Error { get; set; }
    }
}