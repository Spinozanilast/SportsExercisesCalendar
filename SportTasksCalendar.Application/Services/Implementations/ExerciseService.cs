using FluentValidation;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;

namespace SportTasksCalendar.Application.Services.Implementations
{
    public class ExerciseService(IExerciseRepository exerciseRepository, IValidator<Exercise> validator)
        : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;
        private readonly IValidator<Exercise> _validator = validator;

        public async Task<Exercise?> GetExerciseByIdAsync(Guid id)
        {
            return await _exerciseRepository.GetExerciseByIdAsync(id);
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesByCalendarDayIdAsync(Guid calendarDayId)
        {
            return await _exerciseRepository.GetAllCalendarDayExercisesAsync(calendarDayId);
        }


        public async Task AddExerciseAsync(Exercise exercise)
        {
            var validationResult = await _validator.ValidateAsync(exercise);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _exerciseRepository.AddExerciseAsync(exercise);
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            var validationResult = await _validator.ValidateAsync(exercise);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _exerciseRepository.UpdateExerciseAsync(exercise);
        }

        public async Task DeleteExerciseAsync(Guid id)
        {
            await _exerciseRepository.DeleteExerciseAsync(id);
        }
    }
}