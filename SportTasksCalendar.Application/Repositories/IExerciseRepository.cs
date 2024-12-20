﻿using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Repositories
{
    public interface IExerciseRepository
    {
        Task<Exercise?> GetExerciseByIdAsync(Guid id);
        Task<IEnumerable<Exercise>> GetAllCalendarDayExercisesAsync(Guid calendarDayId);
        Task AddExerciseAsync(Exercise exercise);
        Task UpdateExerciseAsync(Exercise exercise);
        Task DeleteExerciseAsync(Guid id);
    }
}