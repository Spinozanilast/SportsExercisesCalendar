using FluentValidation;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;

namespace SportTasksCalendar.Application.Services.Implementations
{
    public class CalendarService(ICalendarRepository calendarRepository, IValidator<Calendar> validator)
        : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;
        private readonly IValidator<Calendar> _validator = validator;

        public async Task<Calendar?> GetCalendarByIdAsync(Guid id)
        {
            return await _calendarRepository.GetCalendarByIdAsync(id);
        }

        public async Task<IEnumerable<Calendar>> GetAllCalendarsAsync()
        {
            return await _calendarRepository.GetAllCalendarsAsync();
        }

        public async Task AddCalendarAsync(Calendar calendar)
        {
            var validationResult = await _validator.ValidateAsync(calendar);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _calendarRepository.AddCalendarAsync(calendar);
        }

        public async Task UpdateCalendarAsync(Calendar calendar)
        {
            var validationResult = await _validator.ValidateAsync(calendar);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _calendarRepository.UpdateCalendarAsync(calendar);
        }

        public async Task DeleteCalendarAsync(Guid id)
        {
            await _calendarRepository.DeleteCalendarAsync(id);
        }
    }
}