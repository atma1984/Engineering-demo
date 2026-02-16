 using System;                                
using System.Collections.Generic;             
using System.IO;                              
using DotNetEnv;
using EngineeringSystem.Backend.Infrastructure.Persistence.Connection; 
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts; 
using Microsoft.EntityFrameworkCore;           
using Microsoft.EntityFrameworkCore.Design;    
using Microsoft.Extensions.Configuration;      

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts
{

    internal class CrmDbContextFactory : IDesignTimeDbContextFactory<CrmDbContext>
    {
        public CrmDbContext CreateDbContext(string[] args)
        {
            var solutionRoot = FindSolutionRootOrFallback();
            var envFileName =
                Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                    ? ".env.docker"
                    : ".env.local";

            var envPath = Path.Combine(solutionRoot, envFileName);
            if (File.Exists(envPath))
            {
                Env.Load(envPath);
            }
            else
            {
                throw new FileNotFoundException(
                    $"Не найден файл окружения '{envFileName}' в корне решения: {envPath}. " +
                    $"Создай файл или убедись, что запускаешь миграции в нужном репозитории.");
            }
            var environment =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
            var sqlOptions = SqlServerConnectionOptions.From(configuration);

            IDbConnectionStringProvider csProvider = new SqlServerConnectionStringProvider(sqlOptions);
            var connectionString = csProvider.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<CrmDbContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                sql =>
                {
                    sql.MigrationsAssembly(typeof(CrmDbContext).Assembly.FullName);
                });

            return new CrmDbContext(optionsBuilder.Options);

        }
        private static string FindSolutionRootOrFallback()
        {
            var current = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (current != null)
            {
                var slnFiles = current.GetFiles("*.sln");
                if (slnFiles.Length > 0)
                {
                    return current.FullName;
                }

                current = current.Parent;
            }

            return Directory.GetCurrentDirectory();
        }
        private static string? FindFileUpwards(string basePath, string fileName)
        {
            var current = new DirectoryInfo(basePath);

            while (current != null)
            {
                var candidate = Path.Combine(current.FullName, fileName);

                if (File.Exists(candidate))
                {
                    return candidate;
                }

                current = current.Parent;
            }

            return null;
        }

        private static void LoadDotEnvIntoEnvironmentVariables(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("#", StringComparison.Ordinal))
                    continue;

                var idx = line.IndexOf('=', StringComparison.Ordinal);

                if (idx <= 0)
                    continue;

                var key = line.Substring(0, idx).Trim();

                var value = line.Substring(idx + 1).Trim();

                value = TrimWrappingQuotes(value);

                Environment.SetEnvironmentVariable(key, value);
            }
        }
        private static string TrimWrappingQuotes(string value)
        {
            if (value.Length < 2)
                return value;

            if (value.StartsWith("\"", StringComparison.Ordinal) &&
                value.EndsWith("\"", StringComparison.Ordinal))
            {
                return value.Substring(1, value.Length - 2);
            }

            if (value.StartsWith("'", StringComparison.Ordinal) &&
                value.EndsWith("'", StringComparison.Ordinal))
            {
                return value.Substring(1, value.Length - 2);
            }

            return value;
        }


    }
}
