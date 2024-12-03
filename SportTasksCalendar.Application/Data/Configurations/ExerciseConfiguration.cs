using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Application.Data.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Category)
            .IsRequired();
        
        builder.Property(e => e.Status) 
            .IsRequired();
    }
}