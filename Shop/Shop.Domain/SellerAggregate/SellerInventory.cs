using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAggregate
{
    public class SellerInventory : BaseEntity
    {
        public long SellerId { get; internal set; }
        public long ProductId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int? DiscountPercentage { get; private set; }

        private SellerInventory()
        {
            
        }


        public SellerInventory(long productId, int count, int price, int? Discountpercentage = null)
        {
            if (price < 1 || count < 0)
            {
                throw new InvalidDomainDataException();
            }

            ProductId = productId;
            Count = count;
            Price = price;
            DiscountPercentage = Discountpercentage;
        }

        public void Edit(int count, int price, int? Discountpercentage = null)
        {
            if (price < 1 || count < 0)
            {
                throw new InvalidDomainDataException();
            }

            Count = count;
            Price = price;
            DiscountPercentage = Discountpercentage;
        }
    }

}
