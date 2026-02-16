using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;
using EngineeringSystem.Backend.Domain.Customers.Contacts;
using EngineeringSystem.Backend.Domain.Geography;

namespace EngineeringSystem.Backend.Domain.Customers
{
    public class Customer : Entity
    {
        public string Name { get; private set; }           // Имя заказчика.
        public List<Region> Regions { get; private set; }  // Связь с регионами, где присутствует заказчик.
        public List<Contact> Contacts { get; private set; } // Контактные лица заказчика.

        private Customer(){}

        public Customer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Имя заказчика обязательно.");

            Name = name;
            Regions = new List<Region>();
            Contacts = new List<Contact>();
        }
        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
        }
        public void AddRegion(Region region)
        {
            Regions.Add(region);
        }
    }
}
