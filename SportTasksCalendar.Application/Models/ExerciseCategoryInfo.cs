using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Application.Models;

public class ExerciseCategoryInfo
{
    public ExerciseCategory Category { get; set; }
    public string Name { get; set; }
    public string ProgressIndicator { get; set; }

    public static ExerciseCategoryInfo[] ExerciseCategories { get; set; }

    static ExerciseCategoryInfo()
    {
        ExerciseCategories = InitCategories();
    }

    private static ExerciseCategoryInfo[] InitCategories()
    {
        return
        [
            new ExerciseCategoryInfo { Category = ExerciseCategory.Run, Name = "Run", ProgressIndicator = "км" },
            new ExerciseCategoryInfo { Category = ExerciseCategory.Swim, Name = "Swim", ProgressIndicator = "мин" },
            new ExerciseCategoryInfo
                { Category = ExerciseCategory.Cycling, Name = "Cycling", ProgressIndicator = "км" },
            new ExerciseCategoryInfo { Category = ExerciseCategory.Yoga, Name = "Yoga", ProgressIndicator = "мин" },
            new ExerciseCategoryInfo
            {
                Category = ExerciseCategory.StrengthTraining, Name = "Strength Training",
                ProgressIndicator = "раз/за подход"
            }
        ];
    }
}