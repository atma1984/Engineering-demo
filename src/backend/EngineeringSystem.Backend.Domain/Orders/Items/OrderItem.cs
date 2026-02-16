using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Orders.Items
{
    public sealed class OrderItem : Entity
    {

        public Guid? NomenclatureItemId { get; private set; }
        public int? Quantity { get; private set; }
        public string Description { get; private set; }
        public bool IsDefined { get; private set; }


        private OrderItem() { }
        internal OrderItem(Guid id, string description)
        {
            Id = id;

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException(
                    "Описание позиции заказа не может быть пустым.");

            Description = description;

            // На момент создания позиция считается НЕ определённой — это просто запрос клиента
            IsDefined = false;
        }

        public void AssignNomenclatureItem(Guid nomenclatureItemId)
        {
            if (nomenclatureItemId == Guid.Empty)
                throw new DomainException(
                    "Идентификатор номенклатуры некорректен.");

            NomenclatureItemId = nomenclatureItemId;

            RecalculateDefinedState();
        }
        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "Количество должно быть больше нуля.");

            Quantity = quantity;

            RecalculateDefinedState();
        }
        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException(
                    "Описание позиции не может быть пустым.");

            Description = description;
        }
        private void RecalculateDefinedState()
        {
            IsDefined =
                NomenclatureItemId.HasValue &&
                Quantity.HasValue;
        }
    }
}
