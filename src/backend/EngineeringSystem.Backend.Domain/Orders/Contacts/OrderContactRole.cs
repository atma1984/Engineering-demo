using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Domain.Orders.Contacts
{
    public enum OrderContactRole
    {
        Primary,        // Основной контакт
        Technical,      // Технический контакт
        DecisionMaker,  // ЛПР
        Financial       // Финансовый контакт
    }
}
