

using Common.Application;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers.Create
{
    public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly ISellerDomainService _domainService;

        public CreateSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService domainService)
        {
            _sellerRepository = sellerRepository;
            _domainService = domainService;
        }

        public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var seller = new Seller(request.UserId, request.ShopName, request.NationalCode, _domainService);

            _sellerRepository.Add(seller);
            await _sellerRepository.Save();
            return OperationResult.Success();
        }
    }
}
