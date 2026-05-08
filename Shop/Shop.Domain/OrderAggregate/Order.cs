using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAggregate.Enums;
using Shop.Domain.OrderAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAggregate
{
    public class Order : AggregateRoot
    {
        private Order()
        {
            
        }

        public Order(long userId)
        {
            UserId = userId;
            Status = OrderStatus.Pending;
            Items = new List<OrderItem>();
        }

        public long UserId { get; set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public OrderShippingMethod? ShippingMethod { get; private set; }
        public DateTime LastUpdate { get; set; }
        public int TotalPrice
        {
            get
            {
                var totalPrice = Items.Sum(i => i.TotalPrice);

                if (ShippingMethod != null)
                {
                    totalPrice += ShippingMethod.ShippingCost;
                }

                if (Discount != null)
                {
                    totalPrice -= Discount.DiscountAmount;
                }

                return totalPrice;
            }
        }
        public int ItemCount => Items.Count;


        public void AddItem(OrderItem item)
        {
            ChangeOrderGuard();

            // ما یکسان باشه OrderItem کد زیر گفته بیا ببین آیتمی پیدا میکنی که ایدی موجودیش با آیدی
            // گفتیم اگر پیدا کردی بیا و تعدادشو بعلاوه تعداد محصولی بکن که از قبل داخل سبد خرید وجود داشته
            // اگرم پیدا نکرد فقط میاد اون موجودی رو اضافه میکنه به لیست خرید
            // میکنه Add یعنی میبینه اگر ایتم رو قبلا داخل سبد خرید داشتیم فقط به تعدادش اضافه میکنه اگر نداشتیم خود آیتم رو
            var oldItem = Items.FirstOrDefault(i => i.InventoryId == item.InventoryId);
            if (oldItem != null)
            {
                oldItem.ChangeCount(item.Count + oldItem.Count);
                return;
            }

            Items.Add(item);
        }

        public void RemoveItem(long itemId)
        {
            ChangeOrderGuard();

            var CurrentItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (CurrentItem != null)
            {
                Items.Remove(CurrentItem);
            }
        }

        public void ChangeCountItem(long itemId, int newCount)
        {
            ChangeOrderGuard();

            var CurrentItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (CurrentItem == null)
            {
                throw new NullOrEmptyDomainDataException();
            }

            CurrentItem.ChangeCount(newCount);
        }

        public void IncreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();

            OrderItem? currentItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (currentItem == null)
            {
                throw new NullOrEmptyDomainDataException();
            }

            currentItem.IncreaseCount(count);
        }

        public void DecreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();

            OrderItem? currentItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (currentItem == null)
            {
                throw new NullOrEmptyDomainDataException();
            }

            currentItem.DecreaseCount(count);
        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }

        public void Checkout(OrderAddress orderAddress)
        {
            ChangeOrderGuard();
            Address = orderAddress;
        }

        private void ChangeOrderGuard()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد");
            }
        }

    }


}
