using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;
using SportTasksCalendar.Application.Services.Implementations;

namespace SportTasksCalendar.Application.Tests.Services
{
    public class CalendarServiceTests
    {
        private readonly Mock<ICalendarRepository> _calendarRepositoryMock = new();
        private readonly Mock<IValidator<Calendar>> _validatorMock = new();
        private readonly CalendarService _sut;

        public CalendarServiceTests()
        {
            _sut = new CalendarService(_calendarRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetAllCalendarsAsync_ShouldReturnAllCalendars()
        {
            // Arrange
            var calendars = new List<Calendar> { new Calendar { Id = Guid.NewGuid(), Name = "Test Calendar" } };
            _calendarRepositoryMock.Setup(repo => repo.GetAllCalendarsAsync())
                .ReturnsAsync(calendars);

            // Act
            var result = await _sut.GetAllCalendarsAsync();

            // Assert
            result.Should().BeEquivalentTo(calendars);
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetCalendarByIdAsync_ShouldReturnCalendar_WhenCalendarExists()
        {
            // Arrange
            var calendarId = Guid.NewGuid();
            var calendar = new Calendar { Id = calendarId, Name = "Test Calendar" };
            _calendarRepositoryMock.Setup(repo => repo.GetCalendarByIdAsync(calendarId))
                .ReturnsAsync(calendar);

            // Act
            var result = await _sut.GetCalendarByIdAsync(calendarId);

            // Assert
            result.Should().BeEquivalentTo(calendar);
        }

        [Fact]
        public async Task GetCalendarByIdAsync_ShouldReturnNull_WhenCalendarDoesNotExist()
        {
            // Arrange
            var calendarId = Guid.NewGuid();
            _calendarRepositoryMock.Setup(repo => repo.GetCalendarByIdAsync(calendarId))
                .ReturnsAsync((Calendar)null);

            // Act
            var result = await _sut.GetCalendarByIdAsync(calendarId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddCalendarAsync_ShouldValidateAndAddCalendar()
        {
            // Arrange
            var calendar = new Calendar
            {
                Id = Guid.NewGuid(), Name = "Test Calendar", StartDate = new DateOnly(2024, 1, 1),
                EndDate = new DateOnly(2024, 12, 31)
            };
            _validatorMock.Setup(v => v.ValidateAsync(calendar, default))
                .ReturnsAsync(new ValidationResult());
            _calendarRepositoryMock.Setup(repo => repo.AddCalendarAsync(calendar))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.AddCalendarAsync(calendar);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(calendar, default), Times.Once);
            _calendarRepositoryMock.Verify(repo => repo.AddCalendarAsync(calendar), Times.Once);
        }

        [Fact]
        public async Task AddCalendarAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var calendar = new Calendar
            {
                Id = Guid.NewGuid(), Name = "", StartDate = new DateOnly(2024, 1, 1),
                EndDate = new DateOnly(2024, 12, 31)
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>
                { new ValidationFailure("Name", "Calendar name cannot be empty.") });
            _validatorMock.Setup(v => v.ValidateAsync(calendar, default))
                .ReturnsAsync(validationResult);

            // Act
            Func<Task> act = async () => await _sut.AddCalendarAsync(calendar);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*Calendar name cannot be empty.*");
        }

        [Fact]
        public async Task UpdateCalendarAsync_ShouldValidateAndUpdateCalendar()
        {
            // Arrange
            var calendar = new Calendar
            {
                Id = Guid.NewGuid(), Name = "Updated Calendar", StartDate = new DateOnly(2024, 1, 1),
                EndDate = new DateOnly(2024, 12, 31)
            };
            _validatorMock.Setup(v => v.ValidateAsync(calendar, default))
                .ReturnsAsync(new ValidationResult());
            _calendarRepositoryMock.Setup(repo => repo.UpdateCalendarAsync(calendar))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.UpdateCalendarAsync(calendar);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(calendar, default), Times.Once);
            _calendarRepositoryMock.Verify(repo => repo.UpdateCalendarAsync(calendar), Times.Once);
        }

        [Fact]
        public async Task DeleteCalendarAsync_ShouldDeleteCalendar()
        {
            // Arrange
            var calendarId = Guid.NewGuid();
            _calendarRepositoryMock.Setup(repo => repo.DeleteCalendarAsync(calendarId))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.DeleteCalendarAsync(calendarId);

            // Assert
            _calendarRepositoryMock.Verify(repo => repo.DeleteCalendarAsync(calendarId), Times.Once);
        }

        // Add more unit tests as needed
    }
}