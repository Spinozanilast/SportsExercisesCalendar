using Microsoft.Extensions.DependencyInjection;

namespace SportTasksCalendar.Application.Data.Extensions;

public static class DbInitializerExtensions
{
    public static void InitializeDb(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
    }
}