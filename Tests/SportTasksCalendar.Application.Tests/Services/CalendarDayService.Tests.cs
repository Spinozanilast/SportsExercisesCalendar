using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;
using SportTasksCalendar.Application.Services.Implementations;

namespace SportTasksCalendar.Application.Tests.Services
{
    public class CalendarDayServiceTests
    {
        private readonly Mock<ICalendarDayRepository> _calendarDayRepositoryMock = new();
        private readonly Mock<IValidator<CalendarDay>> _validatorMock = new();
        private readonly CalendarDayService _sut;

        public CalendarDayServiceTests()
        {
            _sut = new CalendarDayService(_calendarDayRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetCalendarDaysByCalendarId_ShouldReturnCalendarDays()
        {
            // Arrange
            var calendarId = Guid.NewGuid();
            var calendarDays = new List<CalendarDay>
                { new CalendarDay { Id = Guid.NewGuid(), CalendarId = calendarId } };
            _calendarDayRepositoryMock.Setup(repo => repo.GetCalendarDaysByCalendarIdAsync(calendarId))
                .ReturnsAsync(calendarDays);

            // Act
            var result = await _sut.GetCalendarDaysByCalendarId(calendarId);

            // Assert
            result.Should().BeEquivalentTo(calendarDays);
        }

        [Fact]
        public async Task GetCalendarDayByIdAsync_ShouldReturnCalendarDay_WhenCalendarDayExists()
        {
            // Arrange
            var calendarDayId = Guid.NewGuid();
            var calendarDay = new CalendarDay { Id = calendarDayId, Date = new DateOnly(2024, 1, 1) };
            _calendarDayRepositoryMock.Setup(repo => repo.GetCalendarDayByIdAsync(calendarDayId))
                .ReturnsAsync(calendarDay);

            // Act
            var result = await _sut.GetCalendarDayByIdAsync(calendarDayId);

            // Assert
            result.Should().BeEquivalentTo(calendarDay);
        }

        [Fact]
        public async Task GetCalendarDayByIdAsync_ShouldReturnNull_WhenCalendarDayDoesNotExist()
        {
            // Arrange
            var calendarDayId = Guid.NewGuid();
            _calendarDayRepositoryMock.Setup(repo => repo.GetCalendarDayByIdAsync(calendarDayId))
                .ReturnsAsync((CalendarDay)null);

            // Act
            var result = await _sut.GetCalendarDayByIdAsync(calendarDayId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddCalendarDayAsync_ShouldValidateAndAddCalendarDay()
        {
            // Arrange
            var calendarDay = new CalendarDay
                { Id = Guid.NewGuid(), CalendarId = Guid.NewGuid(), Date = new DateOnly(2024, 1, 1) };
            _validatorMock.Setup(v => v.ValidateAsync(calendarDay, default))
                .ReturnsAsync(new ValidationResult());
            _calendarDayRepositoryMock.Setup(repo => repo.AddCalendarDayAsync(calendarDay))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.AddCalendarDayAsync(calendarDay);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(calendarDay, default), Times.Once);
            _calendarDayRepositoryMock.Verify(repo => repo.AddCalendarDayAsync(calendarDay), Times.Once);
        }

        [Fact]
        public async Task AddCalendarDayAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var calendarDay = new CalendarDay { Id = Guid.NewGuid(), CalendarId = Guid.NewGuid(), Date = default };
            var validationResult = new ValidationResult(new List<ValidationFailure>
                { new ValidationFailure("Date", "Date cannot be default value.") });
            _validatorMock.Setup(v => v.ValidateAsync(calendarDay, default))
                .ReturnsAsync(validationResult);

            // Act
            Func<Task> act = async () => await _sut.AddCalendarDayAsync(calendarDay);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*Date cannot be default value.*");
        }

        [Fact]
        public async Task UpdateCalendarDayAsync_ShouldValidateAndUpdateCalendarDay()
        {
            // Arrange
            var calendarDay = new CalendarDay
                { Id = Guid.NewGuid(), CalendarId = Guid.NewGuid(), Date = new DateOnly(2024, 1, 2) };
            _validatorMock.Setup(v => v.ValidateAsync(calendarDay, default))
                .ReturnsAsync(new ValidationResult());
            _calendarDayRepositoryMock.Setup(repo => repo.UpdateCalendarDayAsync(calendarDay))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.UpdateCalendarDayAsync(calendarDay);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(calendarDay, default), Times.Once);
            _calendarDayRepositoryMock.Verify(repo => repo.UpdateCalendarDayAsync(calendarDay), Times.Once);
        }

        [Fact]
        public async Task DeleteCalendarDayAsync_ShouldDeleteCalendarDay()
        {
            // Arrange
            var calendarDayId = Guid.NewGuid();
            _calendarDayRepositoryMock.Setup(repo => repo.DeleteCalendarDayAsync(calendarDayId))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.DeleteCalendarDayAsync(calendarDayId);

            // Assert
            _calendarDayRepositoryMock.Verify(repo => repo.DeleteCalendarDayAsync(calendarDayId), Times.Once);
        }
    }
}