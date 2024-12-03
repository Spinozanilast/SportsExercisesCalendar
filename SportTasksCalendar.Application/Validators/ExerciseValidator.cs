using FluentValidation;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Validators
{
    public class ExerciseValidator : AbstractValidator<Exercise>
    {
        public ExerciseValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Exercise name cannot be empty.")
                .MaximumLength(50).WithMessage("Exercise name cannot exceed 50 characters.");
            RuleFor(e => e.StartDate).LessThan(e => e.EndDate).WithMessage("Start time must be before end time.");
            RuleFor(e => e.Category).IsInEnum().WithMessage("Invalid exercise category.");
        }
    }
}