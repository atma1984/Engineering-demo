using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Customers.Contacts
{
    public sealed class Contact : Entity
    {
        public string FullName { get; private set; } = string.Empty;

        private Contact() { } // для ORM

        public Contact(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("ФИО контакта обязательно.");

            FullName = fullName;
        }
    }
}
