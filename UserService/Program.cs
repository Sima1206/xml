
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UserService.Configuration;
using UserService.Core;
using UserService.Model;
using UserService.Services;
//using Proto1;
using Proto2;

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




builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().Build();
    }
));

//klijent za 4111
//var channel = new Channel("localhost", 4111, ChannelCredentials.Insecure);
//var client = new AccommodationGrpc.AccommodationGrpcClient(channel);
//builder.Services.AddSingleton(client);
builder.Services.AddGrpc();
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
//builder.Services.AddScoped<UserService.Core.IAccommodationService, AccommodationService>();
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

app.UseCors("MyPolicy");


app.MapControllers();
//app.MapGrpcService<UserGrpcService>();
//server za 4112
var server = new Server
{
    Services = { UserGrpc.BindService(new UserGrpcService()) },
    Ports = { new ServerPort("localhost", 4112, ServerCredentials.Insecure) }
};
server.Start();

app.Run();