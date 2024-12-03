using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Services
{
    public interface IExerciseService
    {
        Task<Exercise?> GetExerciseByIdAsync(Guid id);
        Task<IEnumerable<Exercise>> GetAllExercisesByCalendarDayIdAsync(Guid calendarDayId);
        Task AddExerciseAsync(Exercise exercise);
        Task UpdateExerciseAsync(Exercise exercise);
        Task DeleteExerciseAsync(Guid id);
    }
}