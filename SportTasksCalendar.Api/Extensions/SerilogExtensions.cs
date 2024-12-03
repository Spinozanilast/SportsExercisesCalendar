using Serilog;

namespace SportTasksCalendar.Extensions;

public static class SerilogExtensions
{
    public static void UseSerilog(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSerilog((_, loggerConfig) =>
        {
            loggerConfig.WriteTo.Console();
            loggerConfig.ReadFrom.Configuration(configuration);
        });
    }
}