using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Contracts.DTOs;

namespace SportTasksCalendar.Contracts;

public static class MapExtensions
{
    public static Calendar ToEntity(this CalendarRequest calendarRequest, out Guid newCalendarId)
    {
        return new Calendar
        {
            Id = newCalendarId = new Guid(),
            Name = calendarRequest.Name,
            StartDate = calendarRequest.StartDate,
            EndDate = calendarRequest.EndDate,
        };
    }

    public static CalendarDay ToEntity(this CalendarDayRequest calendarDayRequest, out Guid newCalendarDayId)
    {
        return new CalendarDay
        {
            Id = newCalendarDayId = Guid.NewGuid(),
            CalendarId = calendarDayRequest.CalendarId,
            Date = calendarDayRequest.Date,
        };
    }

    public static Exercise ToEntity(this ExerciseRequest exerciseRequest, out Guid newExerciseId)
    {
        return new Exercise
        {
            Id = newExerciseId = Guid.NewGuid(),
            Name = exerciseRequest.Name,
            Description = exerciseRequest.Description,
            CalendarDayId = exerciseRequest.CalendarDayId,
            StartDate = exerciseRequest.StartDate,
            EndDate = exerciseRequest.EndDate,
            Category = exerciseRequest.Category,
            Status = exerciseRequest.Status,
        };
    }
}