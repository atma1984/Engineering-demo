using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Geography
{
    public sealed class Region : Entity
    {
        public string Name { get; private set; } = string.Empty;

        private Region() { } // для ORM

        public Region(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Название региона обязательно.");

            Name = name;
        }
    }
}
