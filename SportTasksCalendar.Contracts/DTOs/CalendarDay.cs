namespace SportTasksCalendar.Contracts.DTOs;

public record CalendarDayRequest(
    DateOnly Date,
    Guid CalendarId,
    List<ExerciseRequest>? SportTasks = null);