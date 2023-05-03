using Microsoft.EntityFrameworkCore;
using UserService.Configuration;
using UserService.Core;
using UserService.Model;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(optionBuilder => {
    optionBuilder.UseSqlServer("Data Source=DESKTOP-7H680CJ;Initial Catalog=User;Integrated Security=true;");
    optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

ProjectConfiguration projectConfiguration = new ProjectConfiguration();
builder.Services.AddSingleton(projectConfiguration);

builder.Services.AddScoped<IUserService, UserService.Services.UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
