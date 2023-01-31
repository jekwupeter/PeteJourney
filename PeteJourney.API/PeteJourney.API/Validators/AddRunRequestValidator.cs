using FluentValidation;

namespace PeteJourney.API.Validators
{
    public class AddRunRequestValidator : AbstractValidator<Models.DTO.AddRunRequest>
    {
        public AddRunRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);

        }
    }
}
