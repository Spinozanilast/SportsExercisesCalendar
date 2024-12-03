namespace SportTasksCalendar.Application.Models;

public class Calendar
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public List<CalendarDay> CalendarDays { get; set; } = [];
}