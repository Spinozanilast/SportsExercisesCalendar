using FluentValidation;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;

namespace SportTasksCalendar.Application.Services.Implementations
{
    public class CalendarDayService(ICalendarDayRepository calendarDayRepository, IValidator<CalendarDay> validator)
        : ICalendarDayService
    {
        private readonly ICalendarDayRepository _calendarDayRepository = calendarDayRepository;
        private readonly IValidator<CalendarDay> _validator = validator;

        public async Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarId(Guid calendarId)
        {
            return await _calendarDayRepository.GetCalendarDaysByCalendarIdAsync(calendarId);
        }

        public async Task<CalendarDay?> GetCalendarDayByIdAsync(Guid id)
        {
            return await _calendarDayRepository.GetCalendarDayByIdAsync(id);
        }

        public async Task<CalendarDay?> GetCalendarDayByIdAsync(DateOnly date, Guid calendarId)
        {
            return await _calendarDayRepository.GetCalendarDayAsync(date, calendarId);
        }

        public async Task AddCalendarDayAsync(CalendarDay calendarDay)
        {
            var validationResult = await _validator.ValidateAsync(calendarDay);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _calendarDayRepository.AddCalendarDayAsync(calendarDay);
        }

        public async Task UpdateCalendarDayAsync(CalendarDay calendarDay)
        {
            var validationResult = await _validator.ValidateAsync(calendarDay);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _calendarDayRepository.UpdateCalendarDayAsync(calendarDay);
        }

        public async Task DeleteCalendarDayAsync(Guid id)
        {
            await _calendarDayRepository.DeleteCalendarDayAsync(id);
        }
    }
}