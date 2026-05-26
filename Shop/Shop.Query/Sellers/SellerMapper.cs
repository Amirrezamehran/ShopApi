using Shop.Domain.SellerAggregate;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers
{
    public static class SellerMapper
    {
        public static SellerDto? Map(this Seller? seller)
        {
            if (seller == null) 
                return null;

            return new SellerDto()
            {
                Id = seller.Id,
                UserId = seller.UserId,
                ShopName = seller.ShopName,
                NationalCode = seller.NationalCode,
                Status = seller.Status,
                CreationDate = seller.CreationDate
            };
        }

        public static SellerDto MapFilterData(this Seller seller)
        {
            if (seller == null) 
                return null;

            return new SellerDto()
            {
                Id = seller.Id,
                UserId = seller.UserId,
                ShopName = seller.ShopName,
                NationalCode = seller.NationalCode,
                Status = seller.Status,
                CreationDate = seller.CreationDate
            };
        }

    }
}
