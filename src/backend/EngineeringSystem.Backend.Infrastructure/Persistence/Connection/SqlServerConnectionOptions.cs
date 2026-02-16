using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.Connection
{
    public sealed class SqlServerConnectionOptions
    {
        public string Host { get; }
        public int Port { get; }
        public string Database { get; }
        public string User { get; }
        public string Password { get; }
        public bool Encrypt { get; }
        public bool TrustServerCertificate { get; }
        public bool MultipleActiveResultSets { get; }
        public bool Pooling { get; }

        private SqlServerConnectionOptions(
                                            string host,
                                            int port,
                                            string database,
                                            string user,
                                            string password,
                                            bool encrypt,
                                            bool trustServerCertificate,
                                            bool multipleActiveResultSets,
                                            bool pooling)
        {
            Host = host;
            Port = port;
            Database = database;
            User = user;
            Password = password;
            Encrypt = encrypt;
            TrustServerCertificate = trustServerCertificate;
            MultipleActiveResultSets = multipleActiveResultSets;
            Pooling = pooling;
        }

        public static SqlServerConnectionOptions From(IConfiguration configuration)
        {
            //  env ключи
            var host = Require(configuration, "OSQUERY_DB_HOST");
            var port = RequireInt(configuration, "OSQUERY_DB_PORT");
            var db = Require(configuration, "OSQUERY_DB_NAME");
            var user = Require(configuration, "OSQUERY_DB_USER");
            var pass = Require(configuration, "OSQUERY_DB_PASSWORD");

            // Дефолты 
            var encrypt = GetBool(configuration, "OSQUERY_DB_ENCRYPT", defaultValue: true);
            var trust = GetBool(configuration, "OSQUERY_DB_TRUST_CERT", defaultValue: true);
            var mars = GetBool(configuration, "OSQUERY_DB_MARS", defaultValue: true);
            var pooling = GetBool(configuration, "OSQUERY_DB_POOLING", defaultValue: true);

            return new SqlServerConnectionOptions(
                host: host,
                port: port,
                database: db,
                user: user,
                password: pass,
                encrypt: encrypt,
                trustServerCertificate: trust,
                multipleActiveResultSets: mars,
                pooling: pooling
            );
        }

        private static string Require(IConfiguration cfg, string key)
        => cfg[key]?.Trim()
           ?? throw new InvalidOperationException($"Missing configuration value: '{key}'");

        private static int RequireInt(IConfiguration cfg, string key)
        {
            var raw = Require(cfg, key);
            if (!int.TryParse(raw, out var value))
                throw new InvalidOperationException($"Configuration value '{key}' must be an integer, but was '{raw}'.");

            if (value <= 0 || value > 65535)
                throw new InvalidOperationException($"Configuration value '{key}' must be in range 1..65535, but was '{value}'.");

            return value;
        }

        private static bool GetBool(IConfiguration cfg, string key, bool defaultValue)
        {
            var raw = cfg[key];
            if (string.IsNullOrWhiteSpace(raw))
                return defaultValue;

            if (bool.TryParse(raw, out var val))
                return val;

            throw new InvalidOperationException($"Configuration value '{key}' must be true/false, but was '{raw}'.");
        }




    }
}
