using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories;

public interface ICalendarDayRepository
{
    Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarIdAsync(Guid calendarId);
    Task<CalendarDay> GetCalendarDayAsync(DateOnly date, Guid calendarId);
    Task AddCalendarDayAsync(CalendarDay calendarDay);
    Task UpdateCalendarDayAsync(CalendarDay calendarDay);
    Task DeleteCalendarDayAsync(DateOnly date, Guid calendarId);
    Task<IEnumerable<CalendarDay>> GetCalendarDaysInRangeAsync(Guid calendarId, DateOnly startDate, DateOnly endDate);
}