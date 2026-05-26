using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers
{
    public class SellerDomainService : ISellerDomainService
    {
        private readonly ISellerRepository _repository;

        public SellerDomainService(ISellerRepository repository)
        {
            _repository = repository;
        }

        public bool CheckSellerInfo(Seller seller)
        {
            // sellerIsExist میریزه تو ظرف true اینجا میاد اگر وجود داشته باشه
            // میده بهمون که ما میفهمیم عملیات ناموفق بوده false و ما گفتیم مخالف اینو برگردون یعنی
            var sellerIsExist = _repository.Exists(s => s.NationalCode == seller.NationalCode || s.UserId == seller.UserId);
            return !sellerIsExist;
        }

        public bool NationalCodeExistInDatabase(string nationalCode)
        {
            return _repository.Exists(s => s.NationalCode == nationalCode);
        }
    }
}
