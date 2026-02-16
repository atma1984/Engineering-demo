using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.Connection
{
    internal class SqlServerConnectionStringProvider : IDbConnectionStringProvider
    {
        private readonly SqlServerConnectionOptions _options;

        public SqlServerConnectionStringProvider(SqlServerConnectionOptions options)
        {
            _options = options;
        }

        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = $"{_options.Host},{_options.Port}",
                InitialCatalog = _options.Database,
                UserID = _options.User,
                Password = _options.Password,

                Encrypt = _options.Encrypt,
                TrustServerCertificate = _options.TrustServerCertificate,

                MultipleActiveResultSets = _options.MultipleActiveResultSets,
                Pooling = _options.Pooling
            };

            return builder.ConnectionString;
        }
    }
}
