using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {
        public string EmailLogin { get; set; }
        public string PasswordLogin { get; set; }
        public string Error { get; set; }
    }
}