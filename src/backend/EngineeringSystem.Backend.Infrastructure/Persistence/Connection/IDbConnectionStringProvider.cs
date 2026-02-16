using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.Connection
{
    public interface IDbConnectionStringProvider
    {
        string GetConnectionString();
    }
}
