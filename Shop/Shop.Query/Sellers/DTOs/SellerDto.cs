using Common.Query;
using Common.Query.Filter;
using Shop.Domain.SellerAggregate.Enums;

namespace Shop.Query.Sellers.DTOs
{
    public class SellerDto : BaseDto
    {
        public long UserId { get; set; }
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
        public SellerStatus Status { get; set; }
    }

    public class SellerFilterParams : BaseFilterParam
    {
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
    }

    // پارامترها فیلتر میکنیم SellerFilterParams رو بر اساس این SellerDto داده های
    // یعنی وقتی اسم فروشگاه یا کد ملی طرفو میزنیم باید مشخصات اون فروشگاه و فرد بیاد بالا
    // البته بهتره پارامترهای دیگه ای هم اضافه کنیم
    public class SellerFilterResult : BaseFilter<SellerDto, SellerFilterParams>
    {

    }
}
