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
    optionBuilder.UseSqlServer("Server=mssql;Database=User;User Id=sa;Password=Your_password123!;");

    optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

ProjectConfiguration projectConfiguration = new ProjectConfiguration();
builder.Services.AddSingleton(projectConfiguration);

builder.Services.AddScoped<IUserService, UserService.Services.UserService>();

var app = builder.Build();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope()) 
{
    var dbContext = scope.ServiceProvider.GetService<ApplicationContext>();
    dbContext.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
