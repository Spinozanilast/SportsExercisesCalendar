using System.Data;
using Microsoft.EntityFrameworkCore;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories.Implementations;

public class CalendarDayRepository(ApplicationDbContext dbContext) : ICalendarDayRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<CalendarDay>> GetCalendarDaysByCalendarIdAsync(Guid calendarId)
    {
        return await _dbContext.CalendarDays
            .AsNoTracking()
            .Where(cd => cd.CalendarId == calendarId)
            .ToListAsync();
    }

    public async Task<CalendarDay?> GetCalendarDayAsync(DateOnly date, Guid calendarId)
    {
        return await _dbContext.CalendarDays
            .AsNoTracking()
            .Where(cd => cd.CalendarId == calendarId && cd.Date == date)
            .FirstOrDefaultAsync();
    }

    public async Task<CalendarDay?> GetCalendarDayByIdAsync(Guid calendarDayId)
    {
        return await _dbContext.CalendarDays
            .AsNoTracking()
            .Where(cd => cd.Id == calendarDayId)
            .FirstOrDefaultAsync();
    }

    public async Task AddCalendarDayAsync(CalendarDay calendarDay)
    {
        _dbContext.CalendarDays.Add(calendarDay);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCalendarDayAsync(CalendarDay calendarDay)
    {
        _dbContext.CalendarDays.Attach(calendarDay);
        var entry = _dbContext.Entry(calendarDay);
        entry.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCalendarDayAsync(Guid calendarDayId)
    {
        var calendarDay = await _dbContext.CalendarDays.FindAsync(calendarDayId);
        if (calendarDay is not null)
        {
            _dbContext.CalendarDays.Remove(calendarDay);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CalendarDay>> GetCalendarDaysInRangeAsync(Guid calendarId, DateOnly startDate,
        DateOnly endDate)
    {
        return await _dbContext.CalendarDays.Where(cd =>
                cd.CalendarId == calendarId && cd.Date >= startDate && cd.Date <= endDate)
            .ToListAsync();
    }
}