using Microsoft.EntityFrameworkCore;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories.Implementations;

public class CalendarRepository(ApplicationDbContext dbContext) : ICalendarRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Calendar>> GetAllCalendarsAsync()
    {
        return await _dbContext.Calendars
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Calendar?> GetCalendarByIdAsync(Guid id)
    {
        return await _dbContext.Calendars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddCalendarAsync(Calendar calendar)
    {
        await _dbContext.Calendars.AddAsync(calendar);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCalendarAsync(Calendar calendar)
    {
        _dbContext.Calendars.Update(calendar);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCalendarAsync(Guid id)
    {
        var calendar = await _dbContext.Calendars.FindAsync(id);
        if (calendar is not null)
        {
            _dbContext.Calendars.Remove(calendar);
            await _dbContext.SaveChangesAsync();
        }
    }
}