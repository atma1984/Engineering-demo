using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Domain.Orders.Notes
{
    public class OrderComment
    {
        public string Comment { get; private set; }

        public OrderComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
                throw new ArgumentException("Comment cannot be empty.");

            Comment = comment;
        }
    }
}
