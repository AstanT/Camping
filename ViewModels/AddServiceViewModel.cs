using Camping.Validators;
using FluentValidation.Attributes;

namespace Camping.ViewModels
{
    [Validator(typeof(AddServiceViewModelValidator))]
    public class AddServiceViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int ClientMax { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Prepaymant { get; set; }
        public string Photo { get; set; }
    }
}