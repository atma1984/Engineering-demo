using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Domain.Orders.Delivery
{
    public class DeliveryExpectation
    {
        public int? Days { get; }
        public string Month { get; }

        public DeliveryExpectation(int days)
        {
            if (days <= 0)
                throw new ArgumentException("Количество дней должно быть больше нуля.", nameof(days));

            Days = days;
            Month = null;
        }


        public DeliveryExpectation(string month)
        {
            if (string.IsNullOrWhiteSpace(month))
                throw new ArgumentException("Месяц не может быть пустым.", nameof(month));

            Month = month;
            Days = null;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DeliveryExpectation;
            return other != null &&
                   Days == other.Days &&
                   Month == other.Month;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Days, Month);
        }

        public override string ToString()
        {
            return Days.HasValue ? $"{Days} days" : Month;
        }
    }
}
