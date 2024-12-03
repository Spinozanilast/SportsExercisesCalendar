namespace SportTasksCalendar.Contracts.DTOs;

public record CalendarRequest(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate
);