using SportTasksCalendar.Application;
using SportTasksCalendar.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseSerilog(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();