using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Data.Configurations;

public class CalendarDayConfiguration : IEntityTypeConfiguration<CalendarDay>
{
    public void Configure(EntityTypeBuilder<CalendarDay> builder)
    {
        builder.HasKey(cd => cd.Id);

        builder.HasIndex(cd => new { cd.Date, cd.CalendarId }).HasDatabaseName("ux_calendar_day").IsUnique();
    }
}