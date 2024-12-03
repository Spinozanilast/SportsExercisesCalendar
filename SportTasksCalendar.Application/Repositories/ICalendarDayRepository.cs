using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories;

public interface ICalendarDayRepository
{
    Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarIdAsync(Guid calendarId);
    Task<CalendarDay?> GetCalendarDayAsync(DateOnly date, Guid calendarId);
    Task<CalendarDay?> GetCalendarDayByIdAsync(Guid calendarDayId);
    Task AddCalendarDayAsync(CalendarDay calendarDay);
    Task UpdateCalendarDayAsync(CalendarDay calendarDay);
    Task DeleteCalendarDayAsync(Guid calendarDayId);
    Task<IEnumerable<CalendarDay>> GetCalendarDaysInRangeAsync(Guid calendarId, DateOnly startDate, DateOnly endDate);
}