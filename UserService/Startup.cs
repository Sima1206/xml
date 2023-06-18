using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Proto2;
using UserService.Configuration;
using UserService.Core;
using UserService.Model;
using UserService.Repository;
using UserService.Services;

namespace UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //dodato
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors();

            services.AddGrpc();


            services.AddDbContext<ApplicationContext>(optionBuilder => {
                optionBuilder.UseSqlServer("Data Source=DESKTOP-7H680CJ;Initial Catalog=Reservation;Integrated Security=true;");
                optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
          //  services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, Services.UserService>();
            var config = new ProjectConfiguration();
            Configuration.Bind("ProjectConfiguration", config);      //  <--- This
            services.AddSingleton(config);
            
            
            
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        
        }

        private Server server;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseCors(builder => builder
        .   AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
            app.UseHttpsRedirection();

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                // Shutdown gRPC channel
                app.ApplicationServices.GetService<Channel>().ShutdownAsync().Wait();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            using (var sScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var cx = sScope.ServiceProvider.GetService<ApplicationContext>();
                cx?.Database.Migrate();
            }
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<UserGrpcService>();

            });

            server = new Server
            {
                Services = { UserGrpc.BindService(new UserGrpcService()) },
                Ports = { new ServerPort("localhost", 4112, ServerCredentials.Insecure) }
            };
            server.Start();

        }
    }
}
