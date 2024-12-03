using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Models.Enums;

namespace SportTasksCalendar.Application.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            if (dbContext.Calendars.Any()) return;

            var calendars = GetInitialCalendars();
            var calendarDays = GetInitialCalendarDays(calendars);
            var exercises = GetInitialExercises(calendarDays);

            dbContext.Calendars.AddRange(calendars);
            dbContext.CalendarDays.AddRange(calendarDays);
            dbContext.Exercises.AddRange(exercises);

            dbContext.SaveChanges();
        }

        private static List<Calendar> GetInitialCalendars()
        {
            var calendars = new List<Calendar>();

            for (var i = 1; i <= 3; i++)
            {
                var calendar = new Calendar
                {
                    Id = Guid.NewGuid(),
                    Name = $"Calendar {i}",
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 12, 31)
                };

                calendars.Add(calendar);
            }

            return calendars;
        }

        private static List<CalendarDay> GetInitialCalendarDays(List<Calendar> calendars)
        {
            var calendarDays = new List<CalendarDay>();

            foreach (var calendar in calendars)
            {
                for (var day = 1; day <= 10; day++)
                {
                    var calendarDay = new CalendarDay
                    {
                        Id = Guid.NewGuid(),
                        Date = new DateOnly(2024, 1, day),
                        CalendarId = calendar.Id
                    };

                    calendarDays.Add(calendarDay);
                }
            }

            return calendarDays;
        }

        private static List<Exercise> GetInitialExercises(List<CalendarDay> calendarDays)
        {
            var exercises = new List<Exercise>();

            foreach (var calendarDay in calendarDays)
            {
                for (var exercise = 1; exercise <= 5; exercise++)
                {
                    var category = (ExerciseCategory)((exercise + calendarDays.IndexOf(calendarDay)) % Enum.GetValues(typeof(ExerciseCategory)).Length);
                    var startTime = new TimeOnly(6, 0).AddHours(exercise);
                    var endTime = startTime.AddMinutes(30);

                    var newExercise = new Exercise
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Exercise {exercise}",
                        Description = $"Description of exercise {exercise} for day {calendarDays.IndexOf(calendarDay)}",
                        StartDate = startTime,
                        EndDate = endTime,
                        Category = category,
                        Status = ExerciseStatus.NotStarted,
                        CalendarDayId = calendarDay.Id
                    };

                    exercises.Add(newExercise);
                }
            }

            return exercises;
        }
    }
}
