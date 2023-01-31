using FluentValidation;

namespace PeteJourney.API.Validators
{
    public class UpdateRunDifficultyRequestValidator : AbstractValidator<Models.DTO.UpdateRunDifficultyRequest>
    {
        public UpdateRunDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();

        }
    }
}
