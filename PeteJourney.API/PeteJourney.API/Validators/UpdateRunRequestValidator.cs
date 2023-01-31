using FluentValidation;

namespace PeteJourney.API.Validators
{
    public class UpdateRunRequestValidator : AbstractValidator<Models.DTO.UpdateRunRequest>
    {
        public UpdateRunRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
