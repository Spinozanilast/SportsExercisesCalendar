using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Repositories;
using SportTasksCalendar.Application.Repositories.Implementations;

namespace SportTasksCalendar.Application;

public static class ApplicationServicesInjector
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        services.AddScoped<ICalendarDayRepository, CalendarDayRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(opt => { opt.UseNpgsql(connectionString); });

        return services;
    }
}