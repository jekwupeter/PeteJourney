using FluentValidation;

namespace PeteJourney.API.Validators
{
    public class ValidateLoginRequest : AbstractValidator<Models.DTO.LoginRequest>
    {
        public ValidateLoginRequest()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
