using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EngineeringSystem.Backend.Domain.Orders
{
    public enum OrderStatus
    {
        Draft = 0,
        Confirmed = 1,
        Completed = 2,
        Closed = 3
    }
}
