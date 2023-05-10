using Microsoft.EntityFrameworkCore;
using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationContext>(optionBuilder => {
    optionBuilder.UseSqlServer("Data Source=DESKTOP-HE4F5VO;Initial Catalog=Reservation;Integrated Security=true;");
    optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

ProjectConfiguration projectConfiguration = new ProjectConfiguration();
builder.Services.AddSingleton(projectConfiguration);

builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IReservationService, ReservationService.Services.ReservationService>();
builder.Services.AddScoped<ITermService, TermService.Services.TermService>();

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
