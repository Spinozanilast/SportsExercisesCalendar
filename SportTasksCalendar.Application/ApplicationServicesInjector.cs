using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportTasksCalendar.Application.Data;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Repositories;
using SportTasksCalendar.Application.Repositories.Implementations;
using SportTasksCalendar.Application.Services;
using SportTasksCalendar.Application.Services.Implementations;
using SportTasksCalendar.Application.Validators;

namespace SportTasksCalendar.Application;

public static class ApplicationServicesInjector
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        services.AddScoped<ICalendarDayRepository, CalendarDayRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        services.AddScoped<ICalendarService, CalendarService>();
        services.AddScoped<ICalendarDayService, CalendarDayService>();
        services.AddScoped<IExerciseService, ExerciseService>();

        services.AddSingleton<IValidator<Calendar>, CalendarValidator>();
        services.AddSingleton<IValidator<CalendarDay>, CalendarDayValidator>();
        services.AddSingleton<IValidator<Exercise>, ExerciseValidator>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(opt => { opt.UseNpgsql(connectionString); });

        return services;
    }
}