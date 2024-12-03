using Microsoft.EntityFrameworkCore;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarDay> CalendarDays { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}