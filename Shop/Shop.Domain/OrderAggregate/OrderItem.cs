using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(long inventoryId, int count, int price)
        {
            GuardCount(count);
            GuardPrice(price);
            InventoryId = inventoryId;
            Count = count;
            Price = price;
        }

        public long OrderId { get; internal set; }
        public long InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int TotalPrice => Price * Count;

        public void IncreaseCount(int count)
        {
            Count += count;
        }

        public void DecreaseCount(int count)
        {
            if (Count == 1)
                return;
            if (Count - count <= 0)
                return;

            Count -= count;
        }

        public void ChangeCount(int newCount)
        {
            GuardCount(newCount);
            Count = newCount;
        }

        public void SetPrice(int newPrice)
        {
            GuardPrice(newPrice);
            Count = newPrice;
        }

        private void GuardCount(int newCount)
        {
            if (newCount < 1)
                throw new InvalidDomainDataException();
        }

        private void GuardPrice(int newPrice)
        {
            if (newPrice < 500)
                throw new InvalidDomainDataException("مبلغ کالا نامعتبر است");
        }

    }

}
