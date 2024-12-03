using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Application.Data.Converters;

public class ExerciseStatusConverter() : ValueConverter<ExerciseStatus, string>(v => v.ToString(),
    v => (ExerciseStatus)Enum.Parse(typeof(ExerciseStatus), v));