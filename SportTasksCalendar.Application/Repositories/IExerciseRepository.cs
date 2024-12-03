using SportTasksCalendar.Application.Models;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetExercisesByCalendarDayIdAsync(Guid calendarDayId);
    Task<Exercise> GetExerciseByIdAsync(Guid id);
    Task AddExerciseAsync(Exercise exercise);
    Task UpdateExerciseAsync(Exercise exercise);
    Task DeleteExerciseAsync(Guid id);

    Task<IEnumerable<Exercise>>
        GetExercisesByDateRangeAsync(Guid calendarDayId, DateOnly startDate,
            DateOnly endDate);
}