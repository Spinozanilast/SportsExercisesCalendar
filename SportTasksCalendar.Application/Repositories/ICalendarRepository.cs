using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories;

public interface ICalendarRepository
{
    Task<IEnumerable<Calendar>> GetAllCalendarsAsync();
    Task<Calendar?> GetCalendarByIdAsync(Guid id);
    Task AddCalendarAsync(Calendar calendar);
    Task UpdateCalendarAsync(Calendar calendar);
    Task DeleteCalendarAsync(Guid id);
}