using FluentValidation;
using Sat.Recruitment.Api.Dto;

namespace Sat.Recruitment.Api.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The email is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
