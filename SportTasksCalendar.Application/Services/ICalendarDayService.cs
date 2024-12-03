using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Services
{
    public interface ICalendarDayService
    {
        Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarId(Guid calendarId);
        Task<CalendarDay?> GetCalendarDayByIdAsync(DateOnly date, Guid calendarId);

        Task<CalendarDay?> GetCalendarDayByIdAsync(Guid id);
        Task AddCalendarDayAsync(CalendarDay calendarDay);
        Task UpdateCalendarDayAsync(CalendarDay calendarDay);
        Task DeleteCalendarDayAsync(Guid id);
    }
}