using Common.Application;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Application.Sellers.Edit
{
    public class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly ISellerDomainService _domainService;

        public EditSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService domainService)
        {
            _sellerRepository = sellerRepository;
            _domainService = domainService;
        }
        public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetTracking(request.Id);
            if (seller == null)
            {
                return OperationResult.NotFound();   
            }

            seller.Edit(request.ShopName, request.NationalCode, _domainService);
            await _sellerRepository.Save();
            return OperationResult.Success();
        }
    }
}
