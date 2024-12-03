namespace SportTasksCalendar.Application.Models;

public class CalendarDay
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid CalendarId { get; set; }
    
    public List<Exercise> SportTasks { get; set; } = [];
    public Calendar Calendar { get; set; }
}