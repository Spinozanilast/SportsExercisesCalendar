using SportTasksCalendar.Application;
using SportTasksCalendar.Application.Data.Extensions;
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
    app.Services.InitializeDb();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();