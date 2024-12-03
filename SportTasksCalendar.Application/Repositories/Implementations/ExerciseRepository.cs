using Microsoft.EntityFrameworkCore;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories.Implementations;

public class ExerciseRepository(ApplicationDbContext dbContext) : IExerciseRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Exercise?> GetExerciseByIdAsync(Guid id)
    {
        return await _dbContext.Exercises.FindAsync(id);
    }

    public async Task<IEnumerable<Exercise>> GetAllCalendarDayExercisesAsync(Guid calendarDayId)
    {
        return await _dbContext.Exercises.Where(e => e.CalendarDayId == calendarDayId).ToListAsync();
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        _dbContext.Exercises.Add(exercise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateExerciseAsync(Exercise exercise)
    {
        _dbContext.Exercises.Attach(exercise);
        var entry = _dbContext.Entry(exercise);
        entry.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteExerciseAsync(Guid id)
    {
        var exercise = await _dbContext.Exercises.FindAsync(id);
        if (exercise is not null)
        {
            _dbContext.Exercises.Remove(exercise);
            await _dbContext.SaveChangesAsync();
        }
    }
}