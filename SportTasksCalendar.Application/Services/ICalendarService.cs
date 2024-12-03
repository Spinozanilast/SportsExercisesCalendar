using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Services
{
    public interface ICalendarService
    {
        Task<Calendar?> GetCalendarByIdAsync(Guid id);
        Task<IEnumerable<Calendar>> GetAllCalendarsAsync();
        Task AddCalendarAsync(Calendar calendar);
        Task UpdateCalendarAsync(Calendar calendar);
        Task DeleteCalendarAsync(Guid id);
    }
}