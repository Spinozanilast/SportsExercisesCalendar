using System.Text.Json.Serialization;
using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Application.Models;

public class Exercise
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }

    public ExerciseCategory Category { get; set; }

    public ExerciseStatus Status { get; set; } = ExerciseStatus.NotStarted;

    public Guid CalendarDayId { get; set; }

    [JsonIgnore] public CalendarDay CalendarDay { get; set; }

    public string GetProgressIndicator()
    {
        var categoryInfo = ExerciseCategoryInfo.ExerciseCategories;
        var category = categoryInfo.FirstOrDefault(x => x.Category == Category);
        return category?.ProgressIndicator ?? String.Empty;
    }
}