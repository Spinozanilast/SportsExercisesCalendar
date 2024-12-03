using FluentValidation;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Validators
{
    public class CalendarDayValidator : AbstractValidator<CalendarDay>
    {
        public CalendarDayValidator()
        {
            RuleFor(cd => cd.Date).NotEqual(default(DateOnly)).WithMessage("Date cannot be default value.");
        }
    }
}