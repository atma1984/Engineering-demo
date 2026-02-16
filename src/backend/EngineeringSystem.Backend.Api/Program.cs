
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts;
using DotNetEnv;
using EngineeringSystem.Backend.Infrastructure;
using EngineeringSystem.Backend.Application.Common;

namespace EngineeringSystem.Backend.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //.env файл (только локальная разработка!)
            if (builder.Environment.IsDevelopment())
            {
                // Если Api в Docker, .env.docker,
                // иначе — .env.local
                var envFile = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                    ? ".env.docker"
                    : ".env.local";

                Env.TraversePath().Load(envFile);
                Console.WriteLine($"Loaded env file: {envFile}");
            }
           
            builder.Configuration.AddEnvironmentVariables();
            builder.Services.AddApi(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            var app = builder.Build();
            await app.UseApiAsync();
            app.Run();
        }
    }
}
