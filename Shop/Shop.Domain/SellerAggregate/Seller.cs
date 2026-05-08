using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAggregate.Enums;
using Shop.Domain.SellerAggregate.Services;


namespace Shop.Domain.SellerAggregate
{
    public class Seller : AggregateRoot
    {
        public long UserId { get; private set; }
        public string ShopName { get; private set; }
        public string NationalCode { get; private set; }
        public SellerStatus Status { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public List<SellerInventory> Inventories { get; private set; }

        private Seller()
        {
            
        }

        public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
        {
            Guard(shopName, nationalCode);
            UserId = userId;
            ShopName = shopName;
            NationalCode = nationalCode;
            Inventories = new List<SellerInventory>();

            // این شرط برای اینه که بعد از پر شدن مقادیر میاد چک میکنه ببینه کاربری با این مشخصات کدملی و نام فروشگاه
            // وجود دارد یا خیر. و اگر وجود داشته باشد نمیذاره کاربر دوباره بیاد فروشگاه بسازه
            if (domainService.CheckSellerInfo(this) == false)
            {
                throw new InvalidDomainDataException("اطلاعات نامعتبر است");
            }
        }

        public void ChangeStatus(SellerStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }

        public void Edit(string shopName, string nationalCode, ISellerDomainService domainService)
        {
            Guard(shopName, nationalCode);
            if (nationalCode != NationalCode)
            {
                if (domainService.NationalCodeExistInDatabase(nationalCode))
                {
                    throw new InvalidDomainDataException("کدملی متعلق به شخص دیگری است");
                }
            }

            ShopName = shopName;
            NationalCode = nationalCode;
        }

        public void AddInventory(SellerInventory inventory)
        {
            if (Inventories.Any(i => i.ProductId == inventory.ProductId))
            {
                throw new InvalidDomainDataException("این محصول قبلا ثبت شده است.");
            }

            Inventories.Add(inventory);
        }

        public void EditInventory(long inventoryId, int count, int price, int? discountPercentage)
        {
            var CurrentInventory = Inventories.FirstOrDefault(i => i.Id == inventoryId);
            if (CurrentInventory == null)
            {
                throw new NullOrEmptyDomainDataException("محصولی برای ویرایش کردن یافت نشد.");
            }

            // To Do Check Inventories
            CurrentInventory.Edit(count, price, discountPercentage);
        }

        public void DeleteInventory(long inventoryId)
        {
            var CurrentInventory = Inventories.FirstOrDefault(i => i.Id == inventoryId);
            if (CurrentInventory == null)
            {
                throw new NullOrEmptyDomainDataException("محصولی برای حذف کردن یافت نشد.");
            }

            Inventories.Remove(CurrentInventory);
        }

        private void Guard(string shopName, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if (IranianNationalIdChecker.IsValid(nationalCode) == false)
            {
                throw new InvalidDomainDataException("کد ملی نامعتبر است");
            }


        }
        
    }


} // End Class
