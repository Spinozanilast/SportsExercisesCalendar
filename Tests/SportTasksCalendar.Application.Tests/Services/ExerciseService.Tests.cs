using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Models.Enums;
using SportTasksCalendar.Application.Repositories;
using SportTasksCalendar.Application.Services.Implementations;

namespace SportTasksCalendar.Application.Tests.Services
{
    public class ExerciseServiceTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock = new();
        private readonly Mock<IValidator<Exercise>> _validatorMock = new();
        private readonly ExerciseService _sut;

        public ExerciseServiceTests()
        {
            _sut = new ExerciseService(_exerciseRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetExerciseByIdAsync_ShouldReturnExercise_WhenExerciseExists()
        {
            // Arrange
            var exerciseId = Guid.NewGuid();
            var exercise = new Exercise { Id = exerciseId, Name = "Test Exercise", Goal = 2};
            _exerciseRepositoryMock.Setup(repo => repo.GetExerciseByIdAsync(exerciseId))
                                   .ReturnsAsync(exercise);

            // Act
            var result = await _sut.GetExerciseByIdAsync(exerciseId);

            // Assert
            result.Should().BeEquivalentTo(exercise);
        }

        [Fact]
        public async Task GetExerciseByIdAsync_ShouldReturnNull_WhenExerciseDoesNotExist()
        {
            // Arrange
            var exerciseId = Guid.NewGuid();
            _exerciseRepositoryMock.Setup(repo => repo.GetExerciseByIdAsync(exerciseId))
                                   .ReturnsAsync((Exercise)null);

            // Act
            var result = await _sut.GetExerciseByIdAsync(exerciseId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllExercisesByCalendarDayIdAsync_ShouldReturnExercises()
        {
            // Arrange
            var calendarDayId = Guid.NewGuid();
            var exercises = new List<Exercise> { new Exercise
                {
                    Id = Guid.NewGuid(),
                    Name = "Morning Run",
                    CalendarDayId = calendarDayId,
                    Goal = 10
                }
            };
            _exerciseRepositoryMock.Setup(repo => repo.GetAllCalendarDayExercisesAsync(calendarDayId))
                                   .ReturnsAsync(exercises);

            // Act
            var result = await _sut.GetAllExercisesByCalendarDayIdAsync(calendarDayId);

            // Assert
            result.Should().BeEquivalentTo(exercises);
        }

        [Fact]
        public async Task AddExerciseAsync_ShouldValidateAndAddExercise()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Test Exercise",
                Goal = 6
            };
            exercise.Id = Guid.NewGuid();
            exercise.StartDate = new TimeOnly(6,
                0);
            exercise.EndDate = new TimeOnly(7,
                0);
            exercise.Category = ExerciseCategory.Run;
            _validatorMock.Setup(v => v.ValidateAsync(exercise, default))
                          .ReturnsAsync(new ValidationResult());
            _exerciseRepositoryMock.Setup(repo => repo.AddExerciseAsync(exercise))
                                   .Returns(Task.CompletedTask);

            // Act
            await _sut.AddExerciseAsync(exercise);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(exercise, default), Times.Once);
            _exerciseRepositoryMock.Verify(repo => repo.AddExerciseAsync(exercise), Times.Once);
        }

        [Fact]
        public async Task AddExerciseAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var exercise = new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "",
                StartDate = new TimeOnly(6,
                    0),
                EndDate = new TimeOnly(7,
                    0),
                Category = ExerciseCategory.Run,
                Goal = 100
            };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Name", "Exercise name cannot be empty.") });
            _validatorMock.Setup(v => v.ValidateAsync(exercise, default))
                          .ReturnsAsync(validationResult);

            // Act
            Func<Task> act = async () => await _sut.AddExerciseAsync(exercise);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                     .WithMessage("*Exercise name cannot be empty.*");
        }

        [Fact]
        public async Task UpdateExerciseAsync_ShouldValidateAndUpdateExercise()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Updated Exercise",
                Goal = 100
            };
            exercise.Id = Guid.NewGuid();
            exercise.StartDate = new TimeOnly(6,
                0);
            exercise.EndDate = new TimeOnly(7,
                0);
            exercise.Category = ExerciseCategory.Run;
            _validatorMock.Setup(v => v.ValidateAsync(exercise, default))
                          .ReturnsAsync(new ValidationResult());
            _exerciseRepositoryMock.Setup(repo => repo.UpdateExerciseAsync(exercise))
                                   .Returns(Task.CompletedTask);

            // Act
            await _sut.UpdateExerciseAsync(exercise);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(exercise, default), Times.Once);
            _exerciseRepositoryMock.Verify(repo => repo.UpdateExerciseAsync(exercise), Times.Once);
        }

        [Fact]
        public async Task DeleteExerciseAsync_ShouldDeleteExercise()
        {
            // Arrange
            var exerciseId = Guid.NewGuid();
            _exerciseRepositoryMock.Setup(repo => repo.DeleteExerciseAsync(exerciseId))
                                   .Returns(Task.CompletedTask);

            // Act
            await _sut.DeleteExerciseAsync(exerciseId);

            // Assert
            _exerciseRepositoryMock.Verify(repo => repo.DeleteExerciseAsync(exerciseId), Times.Once);
        }
    }
}
