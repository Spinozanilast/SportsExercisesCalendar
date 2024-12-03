using System.Data;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories.Implementations;

public class CalendarDayRepository(ApplicationDbContext dbContext) : ICalendarDayRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarIdAsync(Guid calendarId)
    {
        throw new NotImplementedException();
    }

    public Task<CalendarDay> GetCalendarDayAsync(DateOnly date, Guid calendarId)
    {
        throw new NotImplementedException();
    }

    public Task AddCalendarDayAsync(CalendarDay calendarDay)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCalendarDayAsync(CalendarDay calendarDay)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCalendarDayAsync(DateOnly date, Guid calendarId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarDay>> GetCalendarDaysInRangeAsync(Guid calendarId, DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }
}