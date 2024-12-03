using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories.Implementations;

public class ExerciseRepository(ApplicationDbContext dbContext) : IExerciseRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<IEnumerable<Exercise>> GetExercisesByCalendarDayIdAsync(Guid calendarDayId)
    {
        throw new NotImplementedException();
    }

    public Task<Exercise> GetExerciseByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddExerciseAsync(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task UpdateExerciseAsync(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task DeleteExerciseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Exercise>> GetExercisesByDateRangeAsync(Guid calendarDayId, DateOnly startDate,
        DateOnly endDate)
    {
        throw new NotImplementedException();
    }
}