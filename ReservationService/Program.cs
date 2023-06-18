using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using ReservationService;
using ReservationService.Configuration;
using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Repository;
using ReservationService.Services;
using Proto2;
//using Proto1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(optionBuilder => {
    optionBuilder.UseSqlServer("Server=mssql;Database=Reservation;User Id=sa;Password=Your_password123!");
    optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//klijent za 4112
var channel = new Channel("localhost", 4112, ChannelCredentials.Insecure);
var client = new UserGrpc.UserGrpcClient(channel);
builder.Services.AddSingleton(client);
builder.Services.AddGrpc();

builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IReservationService, ReservationService.Services.ReservationService>();
builder.Services.AddScoped<IUserService, ReservationService.Services.UserService>();

var projectConfiguration = new ProjectConfiguration();
builder.Services.AddSingleton(projectConfiguration);

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
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGrpcService<AccommodationGrpcService>();
//});
//server za 4111
//var server = new Server { Services = { AccommodationGrpc.BindService(new AccommodationGrpcService()) }, Ports = { new ServerPort("localhost", 4111, ServerCredentials.Insecure) } };
//server.Start();
//app.Run();
