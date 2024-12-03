using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Application.Data.Converters;

public class ExerciseCategoryConverter() : ValueConverter<ExerciseCategory, string>(v => v.ToString(),
    v => (ExerciseCategory)Enum.Parse(typeof(ExerciseCategory), v));