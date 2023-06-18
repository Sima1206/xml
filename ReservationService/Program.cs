
using Grpc.Net.Client;
using Proto2;
using ReservationService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        var channel = GrpcChannel.ForAddress("https://localhost:4112"); //zapocni channel
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });


}