using FluentValidation;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Validators
{
    public class CalendarValidator : AbstractValidator<Calendar>
    {
        public CalendarValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Calendar name cannot be empty.")
                .MaximumLength(50).WithMessage("Calendar name cannot exceed 50 characters.");
            RuleFor(c => c.StartDate).LessThan(c => c.EndDate).WithMessage("Start date must be before end date.");
        }
    }
}