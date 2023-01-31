using FluentValidation;

namespace PeteJourney.API.Validators
{
    public class AddRunDifficultyRequestValidator : AbstractValidator<Models.DTO.AddRunDifficultyRequest>
    {
        public AddRunDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
