using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Contracts.DTOs;

public record ExerciseRequest(
    string Name,
    string Description,
    Guid CalendarDayId,
    TimeOnly StartDate,
    TimeOnly EndDate,
    ExerciseCategory Category,
    ExerciseStatus Status
);
